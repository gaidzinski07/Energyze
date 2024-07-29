using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceFromBase : MonoBehaviour
{

    public GameManager player;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = "Base: " + (player.distance / 1000).ToString("0.00") + "Km";
    }
}
