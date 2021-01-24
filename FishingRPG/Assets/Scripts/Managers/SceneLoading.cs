using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public Image loadingBar;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
        //Stop Sound -> Stop ambiance explo
        AkSoundEngine.PostEvent("STOP_AMBWind", FishManager.instance.currentFish.gameObject);
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation lvl = SceneManager.LoadSceneAsync(2);

        while(lvl.progress < 1)
        {
            loadingBar.rectTransform.sizeDelta = new Vector2(lvl.progress * 1650, 135);
            yield return new WaitForEndOfFrame();
        }
    }
}
