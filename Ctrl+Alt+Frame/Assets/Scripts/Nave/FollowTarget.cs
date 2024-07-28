using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public Transform player;
    public bool ignoreYAxis;

    void Update()
    {
        Vector3 pos = player.position;
        pos.y = ignoreYAxis ? transform.position.y : pos.y;
        transform.position = pos;
    }
}
