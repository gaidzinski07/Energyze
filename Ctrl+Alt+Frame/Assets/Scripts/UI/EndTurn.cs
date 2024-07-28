using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
    public GameEvent endTurnClick;
    public GameObject buyScreen;
    public GameObject guiCanvas;
    private AudioSource audioSource;
    //private CanvasGroup canvasGUI;

    public void endTurn()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.Play();

        endTurnClick.Raise(this, null);
    }

    public void closeBuyPanel()
    {
        endTurnClick.Raise(this, null);
        //this.canvasGUI = guiCanvas.GetComponent<CanvasGroup>();
        /*CanvasGroup canvasGroup = buyScreen.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = buyScreen.AddComponent<CanvasGroup>();
        }*/
        buyScreen.SetActive(false);
        this.guiCanvas.SetActive(true);
    }
}
