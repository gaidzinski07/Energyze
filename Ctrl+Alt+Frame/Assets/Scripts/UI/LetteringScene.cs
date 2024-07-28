using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LetteringScene : MonoBehaviour
{
    public GameObject loadingObj;
    public Image loadBar;

    public void StartGame()
    {
        StartCoroutine(LoadLevelAsync("Game"));
    }

    public IEnumerator LoadLevelAsync(string level)
    {
        loadingObj.SetActive(true);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(level);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);

            loadBar.fillAmount = progressValue;
            yield return null;
        }
    }

}
