using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int baseLevel = 1;
    public int energyAmount = 1000;
    public int gasAmount = 60;
    public int gasLimit = 60;
    public int totalEnergy;
    public int EngineLevel = 1;
    public int bateryLevel = 1;
    public int hookLevel = 1;
    public int lifes = 10;
    public Image gasBar;

    public float timer;
    public float energyLossRate;
    public float bombAmount = 3;
    public float roofBombAmount = 3;
    private Vector3 centerPosition;

    private float currentHour = 6f;

    [Header("events")]
    public GameEvent collectedEnergyEvent;
    public GameEvent batteryAmountEvent;
    public GameEvent gameOverEvent;
    public GameEvent batteryStatus;
    public GameEvent notificationEvent;

    public Button endTurnButton;

    public TextMeshProUGUI BatteryDataTMP;
    public TextMeshProUGUI EngineDataTMP;
    public TextMeshProUGUI HookDataTMP;
    public TextMeshProUGUI lifeAmountTMP;

    public GameObject lightingManager;

    public float distance;

    private bool notPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        IncreaseEnergyAmount(0);
        IncreaseGas();
        this.timer = 0f;
        this.centerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime; // Incrementa o timer com o tempo decorrido desde o último frame

        // Verifica se passou 1 segundo (ou o intervalo desejado)

        if (this.player == null)
        {
            return;
        }
        this.distance = Vector3.Distance(this.player.transform.position, this.centerPosition);
        //Debug.Log(distance);
        if (this.distance <= 50)
        {
            endTurnButton.gameObject.SetActive(true);
            if (this.timer >= 1f / energyLossRate)
            {
                IncreaseGas();
                this.timer = 0f; // Reinicia o timer
            }
            // Do something when player is inside the radius
        }
        else
        {
            endTurnButton.gameObject.SetActive(false);
            if (this.timer >= 1f / energyLossRate)
            {
                ReduceGas(); // Reduz a energia
                this.timer = 0f; // Reinicia o timer
            }

        }



        if (bateryLevel == 2)
        {
            BatteryDataTMP.text = 2.ToString();
            gasLimit = 120;
        }
        else if (bateryLevel == 3)
        {
            BatteryDataTMP.text = 3.ToString();
            gasLimit = 180;
        }

        if (EngineLevel == 2)
        {
            EngineDataTMP.text = 30.ToString();
            player.gameObject.GetComponent<Nave>().velocidadeMaxima = 30;
        }
        else if (EngineLevel == 3)
        {
            EngineDataTMP.text = 45.ToString();
            player.gameObject.GetComponent<Nave>().velocidadeMaxima = 45;
        }

        if (hookLevel == 2)
        {
            HookDataTMP.text = 20.ToString();
        }
        else if (hookLevel == 3)
        {
            HookDataTMP.text = 30.ToString();
        }


        if(lifes <= 0)
        {
            gameOverEvent.Raise(this, null);
        }
    }

    private void ReduceGas()
    {
        //Debug.Log("Tentando reduzir gas");
        this.gasAmount -= 1; // Reduz a energia em 1 ponto
        gasBar.fillAmount = this.gasAmount;
        // Verifica se a energia chegou a zero
        if (this.gasAmount <= 20 && notPlaying)
        {
            this.GetComponents<AudioSource>()[1].Play();
            notPlaying = false;
            batteryStatus.Raise(this, true);
            notificationEvent.Raise(this, new NotificationSetup("Running out of battery energy. Recharge in base.", "warning"));
        }
        else if (this.gasAmount > 20)
        {
            this.GetComponents<AudioSource>()[1].Stop();
            batteryStatus.Raise(this, true);
            notPlaying = true;
        }


        if (this.gasAmount <= 0)
        {
            //this.GetComponents<AudioSource>()[1].Stop();
            this.gasAmount = 0; // Garante que a energia não seja negativa
            Debug.Log("Energia esgotada!");
            if(energyAmount <= 0)
            {
                DecreaseLife();
            }
            this.lifeAmountTMP.text = lifes.ToString();
            this.gasAmount = this.gasLimit/5;
            //this.transform.position = new Vector3(408, 16, 341);
            //this.lightingManager.GetComponent<LightingManager>().FinalizarDia(this, null);
            this.DecreaseEnergyAmount(200);
            if (lifes == 0)
            {
                Debug.Log("Fim de jogo");
                //Destroy(gameObject);
            }
        }
        batteryAmountEvent.Raise(this, gasAmount);
        //Debug.Log("Gas atual: " + gasAmount);
    }

    private void IncreaseGas()
    {
        gasAmount += 5; // Reduz a energia em 1 ponto

        // Verifica se a energia chegou a zero
        if (gasAmount >= gasLimit)
        {
            gasAmount = gasLimit; // Garante que a energia não seja negativa
        }
        if (this.gasAmount > 30)
        {
            notPlaying = true;
            this.GetComponents<AudioSource>()[1].Stop();
        }
        batteryAmountEvent.Raise(this, gasAmount);
        //Debug.Log("Gas atual: " + gasAmount);
    }

    public void IncreaseEnergyAmount(int amount)
    {
        if(amount > 0)
        {
            notificationEvent.Raise(this, new NotificationSetup(amount.ToString() + " of energy collected!", "default"));
        }
        energyAmount += amount;
        collectedEnergyEvent.Raise(this, energyAmount);
    }

    public void DecreaseEnergyAmount(int amount)
    {
        if(energyAmount > 0)
        {
            notificationEvent.Raise(this, new NotificationSetup(amount.ToString() + " of energy lost!", "warning"));
        }
        energyAmount -= amount;
        energyAmount = Mathf.Clamp(energyAmount, 0, 100000);
        collectedEnergyEvent.Raise(this, energyAmount);
    }

    public void DecreaseLife()
    {
        lifes--;
        if(lifes > 0)
            notificationEvent.Raise(this, new NotificationSetup("1 life lost", "warning"));
    }

    public void checkGameOverOnEndOfDay(Component sender, object data)
    {
        if (distance >= 50) {
            gameOverEvent.Raise(this, null);
        }
    }

    public int GetEnergyAmount()
    {
        return energyAmount;
    }
}
