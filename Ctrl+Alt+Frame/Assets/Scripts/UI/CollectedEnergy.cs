using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectedEnergy : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void alterarEnergiaColetada(Component sender, object data)
    {
        if (data is int)
            text.text = data.ToString();
    }
}
