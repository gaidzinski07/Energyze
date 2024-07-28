using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public TextMeshProUGUI currentBatteryTMP;
    public TextMeshProUGUI currentBatteryRoofTMP;

    public TextMeshProUGUI currentEngineTMP;
    public TextMeshProUGUI currentEngineRoofTMP;


    public TextMeshProUGUI currentHookTMP;
    public TextMeshProUGUI currentHookRoofTMP;

    public TextMeshProUGUI batteryLevelTMP;
    public TextMeshProUGUI engineLevelTMP;
    public TextMeshProUGUI hookLevelTMP;

    public int currentBatteryXp;
    public int roofBatteryXp;

    public int currentEngineXp;
    public int roofEngineXp;

    public int currentHookXp;
    public int roofHookXp;

    private int roofBatteryLv2 = 1000;
    private int roofBatteryLv3 = 2000;

    private int roofEngineLv2 = 1000;
    private int roofEngineLv3 = 2000;

    private int roofHookLv2 = 1000;
    private int roofHookLv3 = 2000;

    private GameManager gameManager;
    private AudioSource BuySource;

    public GameEvent victoryEvent;
    public GameEvent notificationEvent;
    // Start is called before the first frame update
    void Start()
    {
        this.gameManager = GetComponent<GameManager>();
        this.BuySource = GetComponents<AudioSource>()[4];
    }

    private void OnApplyEnergy()
    {
        if (gameManager.bateryLevel == 3 && gameManager.hookLevel == 3 && gameManager.EngineLevel == 3)
        {
            victoryEvent.Raise(this, null);
        }
    }

    public void applyBattery()
    {
        this.BuySource.Play();
        var energy = this.gameManager.GetEnergyAmount();
        var neededBatteryXp = roofBatteryXp - currentBatteryXp;

        this.currentBatteryXp += energy;

        if (currentBatteryXp >= roofBatteryXp)
        {
            notificationEvent.Raise(this, new NotificationSetup("Battery level up!", "success"));
            this.gameManager.DecreaseEnergyAmount(neededBatteryXp);
            currentBatteryXp = 0;
            currentBatteryTMP.text = currentBatteryXp.ToString();
            if (gameManager.bateryLevel == 1)
            {
                roofBatteryXp = this.roofBatteryLv2;
                gameManager.bateryLevel += 1;
                batteryLevelTMP.text = gameManager.bateryLevel.ToString();
                Debug.Log("Bateria nivel" + gameManager.bateryLevel);
            }
            else if (gameManager.bateryLevel == 2)
            {
                roofBatteryXp = this.roofBatteryLv3;
                gameManager.bateryLevel += 1;
                batteryLevelTMP.text = gameManager.bateryLevel.ToString();
            }
            currentBatteryRoofTMP.text = roofBatteryXp.ToString();
        }
        else
        {
            this.gameManager.DecreaseEnergyAmount(energy);
            currentBatteryTMP.text = currentBatteryXp.ToString();
        }
        OnApplyEnergy();
    }

    public void applyEngine()
    {
        this.BuySource.Play();
        var energy = this.gameManager.GetEnergyAmount();
        var neededEngineXp = roofEngineXp - currentEngineXp;

        this.currentEngineXp += energy;

        if (currentEngineXp >= roofEngineXp)
        {
            notificationEvent.Raise(this, new NotificationSetup("Engine level up!", "success"));
            this.gameManager.DecreaseEnergyAmount(neededEngineXp);
            currentEngineXp = 0;
            currentEngineTMP.text = currentEngineXp.ToString();
            if (gameManager.EngineLevel == 1)
            {
                roofEngineXp = this.roofEngineLv2;
                gameManager.EngineLevel += 1;
                engineLevelTMP.text = gameManager.EngineLevel.ToString();
                Debug.Log("Motor nivel" + gameManager.EngineLevel);
            }
            else if (gameManager.EngineLevel == 2)
            {
                roofEngineXp = this.roofEngineLv3;
                gameManager.EngineLevel += 1;
                engineLevelTMP.text = gameManager.EngineLevel.ToString();
            }
            currentEngineRoofTMP.text = roofEngineXp.ToString();
        }
        else
        {
            this.gameManager.DecreaseEnergyAmount(energy);
            currentEngineTMP.text = currentEngineXp.ToString();

        }
        OnApplyEnergy();
    }

    public void applyHook()
    {
        this.BuySource.Play();
        var energy = this.gameManager.GetEnergyAmount();
        var neededHookXp = roofHookXp - currentHookXp;

        this.currentHookXp += energy;

        if (currentHookXp >= roofHookXp)
        {
            notificationEvent.Raise(this, new NotificationSetup("Hook level up!", "success"));
            this.gameManager.DecreaseEnergyAmount(neededHookXp);
            currentHookXp = 0;
            currentHookTMP.text = currentHookXp.ToString();
            if (gameManager.hookLevel == 1)
            {
                roofHookXp = this.roofHookLv2;
                gameManager.hookLevel += 1;
                hookLevelTMP.text = gameManager.hookLevel.ToString();
                Debug.Log("Gancho nivel" + gameManager.hookLevel);
            }
            else if (gameManager.hookLevel == 2)
            {
                roofHookXp = this.roofHookLv3;
                gameManager.hookLevel += 1;
            }
            currentHookRoofTMP.text = roofHookXp.ToString();
        }
        else
        {
            this.gameManager.DecreaseEnergyAmount(energy);
            currentHookTMP.text = currentHookXp.ToString();

        }
        OnApplyEnergy();
    }

}
