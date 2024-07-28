using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRain : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float maxTimeBetweenMeteors = 1f;
    public bool active = true;
    private bool constantlyUpdatedActive = true;
    private Transform player;

    private void Start()
    {
        StartCoroutine(MeteorRainCoroutine());
        player = FindObjectOfType<Nave>().transform;
    }
    private void FixedUpdate()
    {
        float dist = Vector3.Distance(player.position, new Vector3(transform.position.x, player.position.y, transform.position.z));

        //estou considerando que as areas sempre serão equilateras!!!
        constantlyUpdatedActive = dist <= transform.localScale.x / 2;
        if(constantlyUpdatedActive != active)
        {
            active = constantlyUpdatedActive;
        }
    }

    private IEnumerator MeteorRainCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(maxTimeBetweenMeteors);
            if (active)
            {
                Debug.Log("New meteor");
                float x = transform.localScale.x/2;
                float z = transform.localScale.z/2;
                Vector3 position = new Vector3( transform.position.x + Random.Range(x * -1, x), transform.position.y, transform.position.z + Random.Range(z * -1, z));
                Instantiate(meteorPrefab, position, meteorPrefab.transform.rotation);
            }
        }
    }
}
