using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCount : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void alterarDia(Component sender, object data)
    {
        if(data is int)
        {
            int dia = (int)data;
            text.text = dia.ToString().PadLeft(2, '0');
        }
    }
}
