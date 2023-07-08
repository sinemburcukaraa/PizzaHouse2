using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Reflection;
using System;

public class ADManager : MonoBehaviour
{
    BannerView _bannerView;
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadAd();
        });
    }


#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
  private string _adUnitId = "unused";
#endif

    /// <summary>
    /// Creates a 320x50 banner at top of the screen.
    /// </summary>
    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
    }

    public void LoadAd()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    /// <summary>
    /// Destroys the ad.
    /// </summary>
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
}
