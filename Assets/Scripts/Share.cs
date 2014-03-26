using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public sealed class Share : MonoBehaviour
{
    public string lastResponse;
    public string ApiQuery;
    public Texture lastResponseTexture;

    #region FB.Init() example

    private bool isInit = false;
	
	void Start(){
		CallFBInit();
	}

    private void CallFBInit()
    {
        FB.Init(OnInitComplete, OnHideUnity);
    }

    private void OnInitComplete()
    {
		CallFBLogin();
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
        isInit = true;
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("Is game showing? " + isGameShown);
    }

    #endregion

    #region FB.Login() example

    private void CallFBLogin()
    {
        FB.Login("email,publish_actions", LoginCallback);
    }

    void LoginCallback(FBResult result)
    {
        if (result.Error != null)
            lastResponse = "Error Response:\n" + result.Error;
        else if (!FB.IsLoggedIn)
        {
            lastResponse = "Login cancelled by Player";
        }
        else
        {
			CallFBFeed();
            lastResponse = "Login was successful!";
        }
    }

    private void CallFBLogout()
    {
        FB.Logout();
    }
    #endregion

    #region FB.Feed() example

    public string FeedToId = "";
    public string FeedLink = "";
    public string FeedLinkName = "qwddqwqdwqdwdw";
    public string FeedLinkCaption = "qdwqdwqdwqwdqdw";
    public string FeedLinkDescription = "dqwdqwdqwdqwqdwqdwdw";
    public string FeedPicture = "";
    public string FeedMediaSource = "";
    public string FeedActionName = "";
    public string FeedActionLink = "";
    public string FeedReference = "";
    public bool IncludeFeedProperties = false;
    private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

    private void CallFBFeed()
    {
        Dictionary<string, string[]> feedProperties = null;
        if (IncludeFeedProperties)
        {
            feedProperties = FeedProperties;
        }
        FB.Feed(
            link: FeedLink,
            linkName: FeedLinkName,
            linkCaption: FeedLinkCaption,
            linkDescription: FeedLinkDescription,
            callback: Callback
        );
    }

    #endregion



    void Callback(FBResult result)
    {
        lastResponseTexture = null;
        // Some platforms return the empty string instead of null.
        if (!String.IsNullOrEmpty(result.Error))
            lastResponse = "Error Response:\n" + result.Error;
        else if (!ApiQuery.Contains("/picture"))
            lastResponse = "Success Response:\n" + result.Text;
        else
        {
            lastResponseTexture = result.Texture;
            lastResponse = "Success Response:\n";
        }

	}

}

