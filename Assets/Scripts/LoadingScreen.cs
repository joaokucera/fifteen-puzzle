using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SlideAnimation))]
public class LoadingScreen : MonoBehaviour
{
    #region Singleton
    private static LoadingScreen instance = null;

    public static LoadingScreen Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public Material[] backgrounds;
    public Material[] materials;
    public string[] phrases;

    private int index = 0;
    private GameObject loadingBar;
    private TextMesh phrase;
    private AsyncOperation async;
    private float progress = 0.0f;

    void Start()
    {
        loadingBar = transform.GetChild(0).gameObject;
        phrase = transform.GetChild(1).GetComponent<TextMesh>();
    }

    public void Load(string name)
    {
        index = 0;
        loadingBar.renderer.material = materials[index];
        RandomBackground();
        SendMessage("Show");
        StartCoroutine("Loading", name);
    }

    void RandomBackground()
    {
        if (backgrounds.Length > 1)
            renderer.material = backgrounds[Random.Range(0, backgrounds.Length)];
    }

    IEnumerator Loading(string name)
    {
        yield return new WaitForSeconds(0.2f);
        async = Application.LoadLevelAsync(name);   

        while (!async.isDone)
        {
            ChangeLoadMaterial();
            yield return null;
        }

        loadingBar.renderer.material = materials[materials.Length - 1];
        yield return new WaitForSeconds(0.2f);
        SendMessage("Hide");
        async = null;
    }

    void ChangeLoadMaterial()
    {
        progress = async.progress * materials.Length + 1;
        index = (int) Mathf.Clamp(progress, 0, materials.Length - 1);
        loadingBar.renderer.material = materials[index];

        if (phrases.Length > 1)
            phrase.text = phrases[(int)Mathf.Clamp(async.progress * phrases.Length, 0, phrases.Length - 1)];
    }
}
