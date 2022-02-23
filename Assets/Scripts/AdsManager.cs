using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using System;

public class AdsManager : GenericSingleton<AdsManager>, IUnityAdsListener
{
    #region Unity Ads
    public string gameId = "4501958";
	public bool testMode = false;
	public bool isLoaded = false;
	public string placementID = "video";
	#endregion

	#region rewarded Video Data
	public string myRewardedPlacementId = "rewardedVideo";
	public bool isRewardedLoaded = false;
    #endregion

    #region Admob Ads
    public string appID = "ca-app-pub-1579727795693040~4066378144";
	public string bannerID = "ca-app-pub-1579727795693040/7451606300";
	public string interstitialID = "ca-app-pub-1579727795693040/5431430703";
	public string interstitialID_Static = "ca-app-pub-1579727795693040/7866022353";
	public string rewardedID = "ca-app-pub-1579727795693040/2007707932";
	public BannerView bannerView;
	private InterstitialAd interstitial;
	private InterstitialAd interstitial_Static;
	public RewardedAd rewardedAd;
	public bool isRewardedAdLoaded;
	public bool isInterstitialAdLoaded;
	public bool isInterstitialAdLoaded_Static;
    #endregion

    public override void Awake()
	{
		base.Awake();
#if UNITY_IOS
		gameId = "3384380";
#elif UNITY_ANDROID
		gameId = "4501958";
#endif
		Advertisement.AddListener(this);
		Advertisement.Initialize(gameId, testMode);


#if UNITY_IOS
		appID = "ca-app-pub-2814647089623348~1487163640";
		bannerID = "ca-app-pub-2814647089623348/2026219692";
		interstitialID = "ca-app-pub-2814647089623348/9713138025";
		interstitialID_Static = "ca-app-pub-8665645187349876/8584188175";
		rewardedID = "ca-app-pub-2814647089623348/5773893014";

#elif UNITY_ANDROID
		appID = "ca-app-pub-1579727795693040~4066378144s";
		bannerID = "ca-app-pub-1579727795693040/7451606300";
		interstitialID = "ca-app-pub-1579727795693040/5431430703";
		interstitialID_Static = "ca-app-pub-1579727795693040/7866022353";
		rewardedID = "ca-app-pub-1579727795693040/2007707932";
#endif



		MobileAds.Initialize(initStatus => { });
		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appID);
	}
	void Start()
	{
		isLoaded = IsLoaded();

		RequestRewarded();
		RequestBanner();
		RequestInterstitial();
		RequestInterstitial_Static();
	}

	#region interstitial Implementation
	public bool IsLoaded()
	{
		return isLoaded = Advertisement.IsReady(placementID);
	}
	public void ShowInterstitial_Unity()
	{
		Debug.Log("Show Rewarded Video Ad");
		Advertisement.Show(placementID);
	}

	#endregion

	#region Rewarded Video Impletementation
	// Implement IUnityAdsListener interface methods:
	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{

		// Define conditional logic for each ad completion status:
		if (showResult == ShowResult.Finished)
		{
			SetReward();
			IsLoaded();
			// Reward the user for watching the ad to completion.
		}
		else if (showResult == ShowResult.Skipped)
		{

			// Do not reward the user for skipping the ad.
			IsLoaded();
		}
		else if (showResult == ShowResult.Failed)
		{

			IsLoaded();
			Debug.LogWarning("The ad did not finish due to an error.");
		}
	}

	public bool IsRewardedLoaded()
	{
		return isRewardedLoaded;
	}
	public void OnUnityAdsReady(string placementId)
	{
		// If the ready Placement is rewarded, show the ad:
		if (placementId == myRewardedPlacementId)
		{
			isRewardedLoaded = true;

		}
	}
	public void ShowRewardedVideo_Unity()
	{

	
		Debug.Log("Show it to request an ad.");
		if (isRewardedLoaded)
		{
			Advertisement.Show(myRewardedPlacementId);
		}
		else
		{
			Debug.Log("Ad not loaded. Load it to request an ad.");
		}
	}

	public void SetReward()
	{
		Debug.Log(" Rewarded");

		Events.DoFireOnOnSkipLevel();
	}
	public void OnUnityAdsDidError(string message)
	{
		// Log the error.
	}

	public void OnUnityAdsDidStart(string placementId)
	{
		// Optional actions to take when the end-users triggers an ad.
	}

	#endregion


	#region Admob Ads Implementation
	public void RequestInterstitial()
	{


		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd(interstitialID);

		// Called when an ad request has successfully loaded.
		this.interstitial.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		this.interstitial.OnAdOpening += HandleOnAdOpened;
		// Called when the ad is closed.
		this.interstitial.OnAdClosed += HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;



		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);

	}
	public void RequestInterstitial_Static()
	{


		// Initialize an InterstitialAd.
		this.interstitial_Static = new InterstitialAd(interstitialID_Static);

		// Called when an ad request has successfully loaded.
		this.interstitial_Static.OnAdLoaded += HandleOnAdLoaded_Static;
		// Called when an ad request failed to load.
		this.interstitial_Static.OnAdFailedToLoad += HandleOnAdFailedToLoad_Static;
		// Called when an ad is shown.
		this.interstitial_Static.OnAdOpening += HandleOnAdOpened_Static;
		// Called when the ad is closed.
		this.interstitial_Static.OnAdClosed += HandleOnAdClosed_Static;
		// Called when the ad click caused the user to leave the application.
		this.interstitial_Static.OnAdLeavingApplication += HandleOnAdLeavingApplication_Static;



		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial_Static.LoadAd(request);

	}
	public void RequestBanner()
	{

		// Create a 320x50 banner at the top of the screen.
		this.bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Top);

		// Called when an ad request has successfully loaded.
		this.bannerView.OnAdLoaded += this.HandleOnAdLoadedBanner;
		// Called when an ad request failed to load.
		this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoadBanner;
		// Called when an ad is clicked.
		this.bannerView.OnAdOpening += this.HandleOnAdOpenedBanner;
		// Called when the user returned from the app after an ad click.
		this.bannerView.OnAdClosed += this.HandleOnAdClosedBanner;
		// Called when the ad click caused the user to leave the application.
		this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplicationBanner;


		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		this.bannerView.LoadAd(request);
		

	}
	


	#region admob rewared ad events

	public void RequestRewarded()
	{

		this.rewardedAd = new RewardedAd(rewardedID);
		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);


	}




	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
		isRewardedAdLoaded = true;
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	{
		isRewardedAdLoaded = false;
		MonoBehaviour.print(
			"HandleRewardedAdFailedToLoad event received with message: "
			+ args.Message);
	}

	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
			+ args.Message);
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
	}

	public void HandleUserEarnedReward(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		SetReward();
		RequestRewarded();
		MonoBehaviour.print(
			"HandleRewardedAdRewarded event received for "
			+ amount.ToString() + " " + type);
	}

	public void ShowRewardedVideo_Admob()
	{
	

		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
		}
	}

	#endregion


	#region Banners Call Back

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
		isInterstitialAdLoaded = true;
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
			+ args.Message);
		isInterstitialAdLoaded = false;
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
		RequestInterstitial();
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeavingApplication event received");
	}


	public void HandleOnAdLoadedBanner(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleOnAdLoadedBanner event received");
		
	}

	public void HandleOnAdFailedToLoadBanner(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleOnAdFailedToLoadBanner event received with message: "
			+ args.Message);
		
	}

	public void HandleOnAdOpenedBanner(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleOnAdOpenedBanner event received");
	}

	public void HandleOnAdClosedBanner(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleOnAdClosedBanner event received");
		
	}

	public void HandleOnAdLeavingApplicationBanner(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleOnAdLeavingApplicationBanner event received");
	}

	public void HandleOnAdLoaded_Static(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded_Static event received");
		isInterstitialAdLoaded_Static = true;
	}

	public void HandleOnAdFailedToLoad_Static(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd_Static event received with message: "
			+ args.Message);
		isInterstitialAdLoaded_Static = false;
	}

	public void HandleOnAdOpened_Static(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened_Static event received");
	}

	public void HandleOnAdClosed_Static(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed_Static event received");
		RequestInterstitial_Static();
	}

	public void HandleOnAdLeavingApplication_Static(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeavingApplication_Static event received");
	}

	
	#endregion

	public void ShowBanner()
	{
		this.bannerView.Show();
	}
	public void HideBanner()
	{
		this.bannerView.Hide();
	}
	

	public void ShowInterstitial_Admob()
	{
		this.interstitial.Show();
	}
	public void ShowInterstitial()
	{
		if(this.interstitial != null)
        {
			ShowInterstitial_Admob();
        }
		else
        {
			ShowInterstitial_Unity();
        }
	}
	public void ShowRewardedAd()
    {
		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
		}
		else
        {
			Advertisement.Show(myRewardedPlacementId);
		}
	}
	public void ShowInterstitial_Static()
	{
		this.interstitial_Static.Show();
	}
	#endregion
}
