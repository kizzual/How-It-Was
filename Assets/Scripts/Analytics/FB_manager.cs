using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FB_manager : MonoBehaviour
{

    // Awake function from Unity's MonoBehavior
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
        DontDestroyOnLoad(this);
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
    public void FacebookShare()
    {
        FB.ShareLink(new System.Uri("https://play.google.com/store/apps/details?id=com.IndiSpace.HowItwas"), "Рекомендую How it was ? ",
            "");
        //    new System.Uri("http://test.png"));
    }
}
