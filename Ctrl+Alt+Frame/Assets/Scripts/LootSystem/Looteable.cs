using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looteable : MonoBehaviour
{

    public PlayerInput input;
    public string playerTag = "Player";
    private InputAction grabAction;
    public float distanceThreshold = 10f;
    private GameObject playerObject;
    public int lootRarityType = 1;
    public int lootRarityIncrease;
    private string rarity;
    private int energy;
    private bool onAction;
    private string objectName = "cano velho";

    private int commonRoof = 70;
    private int uncommonRoof = 15;
    private int rareRoof = 5;

    private AudioSource CollectaudioSource;

    private AudioSource LootedSource;


    public int totalLootPercent { get; private set; }
    public int CategoryLoot { get; private set; }

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        grabAction = input.actions.FindAction("Grab");
        this.CollectaudioSource = audioSources[0];
    }

    private void OnMouseOver()
    {
        bool pressed = grabAction.ReadValue<float>() > 0 ? true : false;
        if (!onAction && pressed && checkPlayerDistance())
        {
            onAction = true;
            BeginLootCollect();
        }
    }

    private bool checkPlayerDistance()
    {
        playerObject = GameObject.FindWithTag(playerTag);
        if (playerObject == null) return false;
        Transform playerTransform = playerObject.transform;
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= distanceThreshold)
        {
            Debug.Log("O jogador est� dentro da dist�ncia permitida.");
            return true;
        }
        Debug.Log("Longe demais! Chegue mais perto!");
        return false;
    }
    private void BeginLootCollect()
    {
        this.CollectaudioSource.Play();
        this.playerObject.GetComponent<LootManager>().isLooting = true;
        getRarity();
        getEnergy();
        StartCoroutine(LootCoroutine());
    }

    private void getRarity()
    {
        var hookLevel = this.playerObject.GetComponent<GameManager>().hookLevel;
        if (hookLevel == 1)
        {
            this.lootRarityIncrease = Random.Range(0, 5);//0 - 10%
        }
        else if (hookLevel == 2)
        {
            this.lootRarityIncrease = Random.Range(10, 20);//10 - 20%
        }
        else
        {
            this.lootRarityIncrease = Random.Range(20, 30);//20 - 30%
        }

        if (lootRarityType == 1)
        {
            this.commonRoof = 70;
            this.uncommonRoof = 95;
        }
        else if (lootRarityType == 2)
        {
            this.commonRoof = 50;
            this.uncommonRoof = 85;
        }
        else
        {
            this.commonRoof = 30;
            this.uncommonRoof = 70;
        }

        this.CategoryLoot = Random.Range(0, 100);
        if ((this.CategoryLoot + this.lootRarityIncrease) < commonRoof)
        {
            //Debug.Log("Item comum encontrado");
            rarity = "Common";
        }
        else if ((this.CategoryLoot + this.lootRarityIncrease) < uncommonRoof)
        {
            //Debug.Log("Item escasso encontrado! 30% de chance!");
            rarity = "Uncommon";
        }
        else
        {
            //Debug.Log("item raro encontrado! 5% de chance!");
            rarity = "Rare";
        }
        this.totalLootPercent = Mathf.Clamp(this.CategoryLoot + this.lootRarityIncrease, 0, 99);
    }

    private void getEnergy()
    {
        if (rarity == "Common")
        {
            this.energy = Random.Range(50, 70);
        }
        else if (rarity == "Uncommon")
        {
            this.energy = Random.Range(200, 250);
        }
        else
        {
            this.energy = Random.Range(650, 700);
        }
    }

    IEnumerator LootCoroutine()
    {
        Debug.Log("Come�ou a coletar...");
        this.playerObject.GetComponent<CollectBar>().startCollect();
        List<string> lootTypes = new List<string> { "C", "B", "A" };
        this.playerObject.GetComponent<CollectBar>().lootType.text = lootTypes[lootRarityType - 1];
        yield return new WaitForSeconds(3f);
        this.playerObject.GetComponent<CollectBar>().showRarityFeedback(rarity);
        this.playerObject.GetComponents<AudioSource>()[0].Play();
        //Debug.Log("Item " + rarity + " encontrado entre os " + (100 - totalLootPercent) + "% mais raros!");
        //Debug.Log("Acabou de coletar!");

        //Debug.Log("Valor base da coleta: " + (this.CategoryLoot - 10 * this.lootRarityType) + "%");
        //Debug.Log("Amplificador do gancho: " + lootRarityIncrease + "%");
        //Debug.Log("Amplificador do tipo: " + (10 * this.lootRarityType) + "%");

        //Debug.Log("Valor total da coleta: " + totalLootPercent + "%");
        Debug.Log(objectName + " convertido em " + energy + " de energia!");

        this.playerObject.GetComponent<LootManager>().isLooting = false;
        this.playerObject.GetComponent<GameManager>().IncreaseEnergyAmount(energy);
        //TODO caso o jogador ande e cancele, nao deletamos o objeto 
        onAction = false;
        Destroy(gameObject);
    }


}
