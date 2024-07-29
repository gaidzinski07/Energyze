using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    public List<GameObject> allOtherCanvas;
    public GameObject pauseCanvas;
    private GameObject lastMenu;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Pause");
    }

    private void Update()
    {
        float pause = moveAction.ReadValue<float>();
        if(pause != 0 && moveAction.WasPressedThisFrame())
        {
            Pause(this, Time.timeScale == 1);
        }
    }

    public void GameOver(Component sender, object data)
    {
        SceneManager.LoadScene("GameOver");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Victory(Component sender, object data)
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void Pause(Component sender, object data)
    {
        if(data is bool)
        {
            bool pause = (bool)data;
            Time.timeScale = pause ? 0 : 1;
            pauseCanvas.SetActive(pause);
            if (pause)
            {
                foreach(GameObject c in allOtherCanvas)
                {
                    if (c.activeSelf)
                    {
                        lastMenu = c;
                    }
                    c.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject c in allOtherCanvas)
                {
                    c.SetActive(false);
                }
                lastMenu.SetActive(true);
            }
        }
    }
}
