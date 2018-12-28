using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class reklam : MonoBehaviour
{

    InterstitialAd interstitial;

    //static=nesnesini olusturmadan bir kez olsuturup istedgim yerde direk kullanabiliyordum
    static reklam reklamKontrol;

    void Start()
    {
        if (reklamKontrol==null)
        {
            //sahneler arasi geciste objelerin silinmemesini saglayan metod
            DontDestroyOnLoad(gameObject);
            reklamKontrol = this;

            #if UNITY_ANDROID
                        string appId = "ca-app-pub-3940256099942544~3347511713";
            #elif UNITY_IPHONE
                            string appId = "ca-app-pub-3940256099942544~1458002511";
            #else
                                string appId = "unexpected_platform";
            #endif

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(appId);


            #if UNITY_ANDROID
                        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
            #elif UNITY_IPHONE
                            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
            #else
                            string adUnitId = "unexpected_platform";
            #endif

            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId);

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);
        }
        else
        {
            //bir daha olusmamasi icin bu gameobjeyi siliyoruz
            Destroy(gameObject);
        }



    }


    public void reklamiGoster()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        reklamKontrol = null;
        Destroy(gameObject);
    }
}
