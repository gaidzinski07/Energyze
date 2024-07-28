using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{

    public Transform radarBar;
    [Range(1, 360)]
    public float barRotationSpeed;

    // Update is called once per frame
    void Update()
    {
        radarBar.eulerAngles -= new Vector3(0, 0, barRotationSpeed * Time.deltaTime);
    }
}
