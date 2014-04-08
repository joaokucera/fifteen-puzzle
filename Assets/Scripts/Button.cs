using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	[SerializeField]
	private Material buttonUp;
	[SerializeField]
	private Material buttonDown;
	[SerializeField]
	private AudioClip audioButtonClick;
	[SerializeField]
	private bool exit = false;
	[SerializeField]
	private bool changeScene = false;
	[SerializeField]
	private string nextSceneToLoad = string.Empty;
	[SerializeField]
	private string functionName = string.Empty;
    [SerializeField]
    private bool parent = false;
    [SerializeField]
    private string objectTag = string.Empty;

	void Start()
	{
		if (buttonUp)
			renderer.material = buttonUp;
	}
	
	void OnMouseUp () 
	{
		if (buttonUp)
			renderer.material = buttonUp;
	}

	void OnMouseDown () 
	{
		if (buttonDown)
			renderer.material = buttonDown;

		if (audioButtonClick)
			AudioSource.PlayClipAtPoint (audioButtonClick, transform.position);

        if (exit)
            Application.Quit();
        else if (changeScene)
            LoadingScreen.Instance.Load(nextSceneToLoad);
        else
        {
            if (!string.Empty.Equals(objectTag))
                GameObject.FindGameObjectWithTag(objectTag).SendMessage(functionName);
            else if (parent)
                SendMessageUpwards(functionName);
            else
                SendMessage(functionName);
        }
			
	}
}