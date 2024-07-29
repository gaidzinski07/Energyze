using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> menus;
    public Animator anim;
    public Image loadBar;

    public void Start()
    {
        Time.timeScale = 1;
        Debug.Log("começou");
        anim.SetTrigger("Main");
    }

    public void changeMenu(GameObject targetMenu)
    {
        ResetAllTriggers();
        if (menus.Contains(targetMenu)) {
            if(targetMenu.name == "MainMenu")
            {
                anim.SetTrigger("Main");
            }
            else
            {
                anim.SetTrigger("Secondary");
            }
            foreach(GameObject m in menus)
            {
                m.SetActive(false);
                if (m.Equals(targetMenu))
                {
                    m.SetActive(true);
                }
            
            }
        }
    }

    public void StartGame()
    {
        anim.SetTrigger("Start");
        Debug.Log("COMEÇOU");
        StartCoroutine(LoadLevelAsync("StartLettering"));
    }

    public void Exit()
    {
        Application.Quit();
    }

    public IEnumerator LoadLevelAsync(string level)
    {
        yield return new WaitForSeconds(0.7f);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(level);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);

            loadBar.fillAmount = progressValue;
            yield return null;
        }
    }

    private void ResetAllTriggers()
    {
        foreach (var param in anim.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                anim.ResetTrigger(param.name);
            }
        }
    }
}
