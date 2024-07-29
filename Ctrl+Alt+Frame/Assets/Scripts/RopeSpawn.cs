using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    GameObject partPrefab, parentObject;

    [Range(1, 1000)]
    public int length = 1;

    public float partDistance = 0.2f;

    public bool reset, spawn, snapFirst, snapLast;

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            foreach(GameObject tmp in GameObject.FindGameObjectsWithTag("Rope"))
            {
                Destroy(tmp);
            }
        }
    }

    public void Spawn()
    {
        int count = (int) (length / partDistance);

        for(int x = 0; x > count; x++)
        {

        }

    }
}
