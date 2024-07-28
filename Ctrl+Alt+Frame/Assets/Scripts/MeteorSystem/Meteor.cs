using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public float velocity;
    public bool goingDown = true;
    private bool wasGoingDown = true;
    public GameObject rock;
    public GameObject explosion;
    public GameObject radarSprite;
    private GameObject radarSpriteSceneObject;
    public float craterLifeTime;
    public float damageRay;
    private GameObject player;


    private void Start()
    {

        player = FindObjectOfType<Nave>().gameObject;

        // Bit shift the index of the layer (8) to get a bit mask
        // This will cast rays only against colliders in layer 8.
        int layerMask = 1 << 8;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            radarSpriteSceneObject = Instantiate(radarSprite, hit.point, radarSprite.transform.rotation);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (goingDown)
        {
            transform.Translate(Vector3.down * Time.deltaTime * velocity);
        }
        wasGoingDown = goingDown;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Terrain")
        {
            Explode();
        }
    }

    public void Explode()
    {
        //visual stuff
        goingDown = false;
        rock.SetActive(false);
        explosion.SetActive(true);
        explosion.transform.position = radarSpriteSceneObject.transform.position;
        Destroy(gameObject, craterLifeTime);
        Destroy(radarSpriteSceneObject);
        //logical stuff
        float dist = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Player distance is: " + dist);
        if(dist <= damageRay)
        {
            Debug.Log("Hit player! Haha");
        }

    }

}
