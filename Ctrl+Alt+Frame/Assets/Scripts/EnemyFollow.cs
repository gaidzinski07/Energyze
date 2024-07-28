using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player; // Referência ao Transform do jogador
    public float speed = 5f; // Velocidade de movimento do inimigo
    public float detectionRadius = 40f; // Raio de detecção do inimigo
    public float damageRadius = 7f; // Raio de detecção do inimigo

    public bool hasnStartedDamage = true;
    public float distanceToPlayer;
    private int playerGas;

    public GameEvent batteryAmountEvent;
    private AudioSource DamageAudioSource;

    private void Start()
    {
        this.playerGas = player.GetComponent<GameManager>().gasAmount;
        this.DamageAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        this.distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer <= damageRadius)
            {
                if (hasnStartedDamage)
                {
                    StartCoroutine(ApplyDamageCoroutine());
                }
            }
            else
            {
                this.transform.LookAt(this.player.transform);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

        }
        else
        {
            hasnStartedDamage = true;
        }

    }
    IEnumerator ApplyDamageCoroutine()
    {
        hasnStartedDamage = false;
        DamageAudioSource.Play();
        player.GetComponent<GameManager>().gasAmount -= 10;
        yield return new WaitForSeconds(2f);
        if (distanceToPlayer <= damageRadius)
        {
            StartCoroutine(ApplyDamageCoroutine());
        }
        else
        {
            hasnStartedDamage = true;
        }
    }
}
