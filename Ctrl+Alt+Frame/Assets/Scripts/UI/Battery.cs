using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    public Image bar;
    private Color normalColor;
    public Color warningColor = Color.red;

    private void Awake()
    {
        normalColor = bar.color;
    }

    public void changeBarFillAmount(Component sender, object data)
    {
        var playerObject = GameObject.FindWithTag("Player");
        var gasLimit = playerObject.GetComponent<GameManager>().gasLimit;
        if (data is int)
        {
            bar.color = normalColor;
            int barAmount = (int)data;
            bar.fillAmount = barAmount / (float)gasLimit;
            if(barAmount <= 20)
            {
                bar.color = warningColor;
            }
        }
    }

}
