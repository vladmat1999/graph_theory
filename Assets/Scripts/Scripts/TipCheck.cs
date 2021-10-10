using UnityEngine;
using UnityEngine.Advertisements;

public class TipCheck : MonoBehaviour
{
    public Tip tip;
    public PopUp popUp;
    public void OnMouseDown()
    {
        Audio.playClick();
        
        if(Advertisement.IsReady())
        {
            if(AdManager.adsEnabled)
            {
                ShowAd();
                popUp.deactivate();
            }
            else
            {
                tip.reset();
                popUp.deactivate();
            }
        }
    }

    void ShowAd ()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show(AdManager.rewardedVideo, options);
    }

    void HandleShowResult (ShowResult result)
    {
        if(result == ShowResult.Finished) 
        {
            tip.reset();
        }
    }
}
