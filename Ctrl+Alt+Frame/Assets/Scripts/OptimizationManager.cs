using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptimizationManager : MonoBehaviour
{

    [SerializeField]
    [Min(0)]
    public float maxDistance = 100;

    public string[] tags;
    private List<GameObject> trackedObjs = new List<GameObject>();
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        for (int x = 0; x < tags.Length; x++)
        {
            trackedObjs.AddRange(GameObject.FindGameObjectsWithTag(tags[x]));
        }
        StartCoroutine(OneSecondUpdate());
    }

    private IEnumerator OneSecondUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < trackedObjs.Count; i++)
            {
                float distanciaDoPlayer = Vector3.Distance(player.transform.position, trackedObjs[i].transform.position);
                if (distanciaDoPlayer >= maxDistance)
                {
                    trackedObjs[i].SetActive(false);
                    continue;
                }
                trackedObjs[i].SetActive(true);
            }
        }
    }
}