using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public int sceneIndex;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation(sceneIndex));
    }

    IEnumerator LoadAsyncOperation(int _scene)
    {
        AsyncOperation lvl = SceneManager.LoadSceneAsync(_scene);

        while(lvl.progress < 1)
        {

        }

        yield return new WaitForEndOfFrame();
    }
}
