using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CC_AdMob
{
    public class AdMobObject : MonoBehaviour
    {
        internal Action<InitializationStatus> initAction;
        internal InterstitialAd interstitialAd;
        internal BannerView bannerView;
        internal bool debugging;

        private const string debugBannerAdUnitIdAndroid = "ca-app-pub-3940256099942544/6300978111"; //test id
        private const string debugBannerAdUnitIdIOS = "ca-app-pub-3940256099942544/2934735716";
        private const string defaultFallBackAdUnitId = "unexpected_platform";
        
        private string bannerAdUnitAndroid = "";
        private string bannerAdUnitIOS = "";

        public string BannerAdUnitIdAndroid
        {
            set
            {
                bannerAdUnitAndroid = value;
            }
            get
            {
                if (debugging)
                    return debugBannerAdUnitIdAndroid;
                return bannerAdUnitAndroid;
            }
        }
        public string BannerAdUnitIdIOS
        {
            set
            {
                bannerAdUnitIOS = value;
            }
            get
            {
                if (debugging)
                    return debugBannerAdUnitIdIOS;
                return bannerAdUnitIOS;
            }
        }
        
        public void Start()
        {
            if (initAction == null)
                MobileAds.Initialize(initStatus => { });
            else
                MobileAds.Initialize(initAction);
        }

        public void RequestBanner(AdSize adSize, AdPosition adPos)
        {
#if UNITY_ANDROID
            string adUnitId = BannerAdUnitIdAndroid;
#elif UNITY_IPHONE
            string adUnitId = BannerAdUnitIdIOS;
#else
            string adUnitId = defaultFallBackAdUnitId;
#endif
            bannerView = new BannerView(adUnitId, adSize, adPos);

            bannerView.OnAdLoaded += HandleOnBannerAdLoaded;
            bannerView.OnAdFailedToLoad += HandleOnBannerAdFailedToLoad;
            bannerView.OnAdOpening += HandleOnBannerAdOpened;
            bannerView.OnAdClosed += HandleOnBannerAdClosed;
            bannerView.OnAdLeavingApplication += HandleOnBannerAdLeavingApplication;

            AdRequest request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
        }

        public void RemoveBanner()
        {
            bannerView.Destroy();
        }



        #region Events
        public virtual void HandleOnBannerAdLoaded(object sender, EventArgs e) =>
            MonoBehaviour.print("HandleOnBannerAdLoaded event received");

        public virtual void HandleOnBannerAdLeavingApplication(object sender, EventArgs e) =>
            MonoBehaviour.print("HandleOnBannerAdLeavingApplication event received");

        public virtual void HandleOnBannerAdClosed(object sender, EventArgs e) =>
            MonoBehaviour.print("HandleOnBannerAdClosed event received");

        public virtual void HandleOnBannerAdOpened(object sender, EventArgs e) =>
            MonoBehaviour.print("HandleOnBannerAdOpened event received");

        public virtual void HandleOnBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs e) =>
            MonoBehaviour.print($"HandleOnBannerAdFailedToLoad event received with message: {e.Message}");

        #endregion
    }
}
