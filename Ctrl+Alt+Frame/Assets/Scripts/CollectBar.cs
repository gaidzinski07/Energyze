using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectBar : MonoBehaviour
{
    public Image bar;
    public GameObject barObj;
    public TextMeshProUGUI lootType;
    public GameObject rarityFeedback;
    public TextMeshProUGUI rarityFeedbackTMP;

    private int fill = 0;

    public void startCollect()
    {
        barObj.SetActive(true);
        StartCoroutine(collectCoroutine());
    }

    public void showRarityFeedback(string rarity)
    {
        rarityFeedback.SetActive(true);
        rarityFeedbackTMP.text = rarity;
        Invoke("closePanel", 3);
    }

    public void closePanel()
    {
        rarityFeedback.SetActive(false);
    }

    IEnumerator collectCoroutine()
    {
        fill += 1;
        bar.fillAmount = fill / 25f;
        yield return new WaitForSeconds(0.1f);
        if (fill < 25f)
        {
            StartCoroutine(collectCoroutine());
        }
        else
        {
            fill = 0;
            barObj.SetActive(false);
        }

    }
}