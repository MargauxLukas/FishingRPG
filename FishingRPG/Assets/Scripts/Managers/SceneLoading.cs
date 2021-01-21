using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public static SceneLoading instance;

    public Image loadingBar;
    public int sceneIndex;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(LoadAsyncOperation(sceneIndex));
    }

    IEnumerator LoadAsyncOperation(int _scene)
    {
        AsyncOperation lvl = SceneManager.LoadSceneAsync(_scene);

        while(lvl.progress < 1)
        {
            loadingBar.rectTransform.sizeDelta = new Vector2(lvl.progress * 1650, 135);
        }

        yield return new WaitForEndOfFrame();
    }
}
