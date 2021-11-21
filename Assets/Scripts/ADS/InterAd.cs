using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterAd : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private string InterstitiaUnitId = "ca-app-pub-9461918014763845/4258500203";

    private void OnEnable()
    {
        interstitialAd = new InterstitialAd(InterstitiaUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }
    public void ShowAd()
    {
        if (interstitialAd.IsLoaded())
            interstitialAd.Show();
    }
}
