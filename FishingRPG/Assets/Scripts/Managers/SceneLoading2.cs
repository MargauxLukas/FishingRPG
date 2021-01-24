﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading2 : MonoBehaviour
{
    public Image loadingBar;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation lvl = SceneManager.LoadSceneAsync(0);

        while (lvl.progress < 1)
        {
            loadingBar.rectTransform.sizeDelta = new Vector2(lvl.progress * 1650, 135);
            yield return new WaitForEndOfFrame();
        }
    }
}
