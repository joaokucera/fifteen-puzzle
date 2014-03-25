using UnityEngine;

public class Admob : MonoBehaviour
{
#if UNITY_ANDROID

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	void OnStart()
	{
		AdvertisementHandler.HideAds();
		AdvertisementHandler.DisableAds();
	}

    void OnEnable()
    {
        Hole.OnStartShuffle += OnStartShuffle;
        Hole.OnMoveBlock += OnMoveBlock;
        GameOver.OnGameOver += OnGameOver;
    }

    void OnDisable()
    {
        Hole.OnStartShuffle -= OnStartShuffle;
        Hole.OnMoveBlock -= OnMoveBlock;
        GameOver.OnGameOver -= OnGameOver;

        AdvertisementHandler.HideAds();
        AdvertisementHandler.DisableAds();
    }

    void OnStartShuffle()
    {
        AdvertisementHandler.EnableAds();
        AdvertisementHandler.ShowAds();
    }

    void OnGameOver()
    {
        AdvertisementHandler.EnableAds();
        AdvertisementHandler.ShowAds();
    }

    void OnMoveBlock()
    {
        AdvertisementHandler.HideAds();
        AdvertisementHandler.DisableAds();
    }
#endif
}
