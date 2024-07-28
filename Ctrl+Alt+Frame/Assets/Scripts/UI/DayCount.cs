using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public LightingManager lm;



    public void alterarDia(Component sender, object data)
    {
        Debug.Log("entrou!");
        if(data is int)
        {
            int dia = (int)data;
            text.text = dia.ToString().PadLeft(2, '0');
        }
    }

    private void OnEnable()
    {
        text.text = lm.dia.ToString();
    }
}
