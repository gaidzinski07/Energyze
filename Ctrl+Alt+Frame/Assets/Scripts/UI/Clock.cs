using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{

    public TextMeshProUGUI text;

    public void alterarHorario(Component sender, object data)
    {
        if(data is float)
        {
            float timeOfDay = (float)data;
            int hour = (int)timeOfDay;
            float minute = (timeOfDay - hour) * 60;
            text.text = hour.ToString().PadLeft(2, '0') + ":" + ((int)minute).ToString().PadLeft(2, '0');
        }
    }

}
