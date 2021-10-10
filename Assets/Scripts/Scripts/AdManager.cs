using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    public static string gameId = "3237531";
    public static string banner = "Banner";
    public static string video = "video";
    public static string rewardedVideo = "rewardedVideo";
    public static string popup = "LevelComplete";
    public bool testMode = false;
    public static bool adsEnabled = false;

    void Start () {
        Advertisement.Initialize (gameId, testMode);
        StartCoroutine (ShowBannerWhenReady ());
    }

    IEnumerator ShowBannerWhenReady () {
        while (!Advertisement.IsReady (banner) && !adsEnabled) {
            yield return new WaitForSeconds (0.5f);
        }
        //Advertisement.Show(banner);
    }

    public static void showAd()
    {
        if(adsEnabled)
            if(LevelManager.levelsSoFar == 10)
            {
                if(Advertisement.IsReady(popup))
                    Advertisement.Show(popup);
            }
            else
            {
                int rnd = Random.next(8,14);

                if(LevelManager.levelsSoFar % 10 > rnd)
                {
                    if(Random.next(0,3) > 0)
                    {
                        if(Advertisement.IsReady(video))
                            Advertisement.Show(video);
                    }
                    else
                    {
                        if(Advertisement.IsReady(video))
                            Advertisement.Show(video);
                    }
                    
                    LevelManager.levelsSoFar = 20;
                }
            }
    }
}
