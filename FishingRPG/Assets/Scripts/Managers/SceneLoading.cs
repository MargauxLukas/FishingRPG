using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public Image loadingBar;
    public int sceneIndex;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
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
