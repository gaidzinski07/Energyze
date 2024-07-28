using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightingManager : MonoBehaviour
{
    public Light directionalLight;
    public LightingPreset preset;
    [Range(0,1)]
    public float timeScale;
    private float m_timeScale;
    [Range(0, 24)]public float timeOfDay;

    [Header("Configuração de dia e noite")]
    [Range(1, 23)]
    public float inicioDoDia = 6;
    [Range(1, 23)]
    public float fimDoDia = 19;

    public int dia = 0;
    private bool disparouNotificacaoFimDia = false;

    [Header("Event")]
    public GameEvent onDayChanged;
    public GameEvent onClockChanged;
    public GameEvent notificationHappened;
    public GameEvent endOfDayEvent;

    public GameObject buyScreen;
    public GameObject guiCanvas;
    private CanvasGroup canvasGroup;
    private CanvasGroup canvasGUI;

    public GameObject audioSourceObj;
    private AudioSource audioSourceCanvas;
    private AudioSource audioSourceSound;

    private GameManager gm;

    private void Start()
    {
        m_timeScale = timeScale;

        this.gm = GameObject.FindWithTag("Player").GetComponent<GameManager>();
        this.canvasGUI = guiCanvas.AddComponent<CanvasGroup>();

        this.canvasGroup = buyScreen.GetComponent<CanvasGroup>();
        if (this.canvasGroup == null)
        {
            this.canvasGroup = buyScreen.AddComponent<CanvasGroup>();

        }
        this.canvasGroup.gameObject.SetActive(false);
        this.audioSourceCanvas = GetComponents<AudioSource>()[0];
        this.audioSourceSound = GetComponents<AudioSource>()[1];

    }

    private void Update()
    {
        if(preset == null)
        {
            return;
        }
        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * m_timeScale;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay / 24f);
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }

        if(timeOfDay >= fimDoDia - 1.5 && !disparouNotificacaoFimDia)
        {
            disparouNotificacaoFimDia = true;
            notificationHappened.Raise(this, new NotificationSetup("The day is ending, go back to the base!", "warning"));
        }

        if(timeOfDay >= fimDoDia)
        {
            //FinalizarDia(null, null);
            endOfDayEvent.Raise(this, null);
        }
        
        onClockChanged.Raise(this, timeOfDay);
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if(directionalLight != null)
        {
            directionalLight.color = preset.DirectionalLightColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170, 0));
        }

    }

    public void FinalizarDia(Component sender, object data)
    {
        disparouNotificacaoFimDia = false;
        this.audioSourceCanvas.Play();
        this.audioSourceSound.Play();
        Debug.Log("Fim do dia!");
        timeOfDay = (fimDoDia - 1);
        UpdateLighting(timeOfDay / 24f);
        notificationHappened.Raise(this, new NotificationSetup("Good night! Don't forget to upgrade the ship.", "default"));
        PauseTimeCount(true);
        this.canvasGUI.gameObject.SetActive(false);
        this.canvasGroup.gameObject.SetActive(true);
        this.gm.bombAmount = this.gm.roofBombAmount;
    }

    public void ComecarNovoDia()
    {
        disparouNotificacaoFimDia = false;
        Debug.Log("Bom dia!");
        onDayChanged.Raise(this, ++dia);
        timeOfDay = inicioDoDia;
        notificationHappened.Raise(this, new NotificationSetup("Good morning! Starting day " + dia.ToString().PadLeft(2, '0'), "default"));
        PauseTimeCount(false);
    }

    public void PauseTimeCount(bool pause)
    {
        m_timeScale = pause ? 0 : timeScale;
    }

    private void OnValidate()
    {
        if (directionalLight != null)
            return;
        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light l in lights)
            {
                if(l.type == LightType.Directional)
                {
                    directionalLight = l;
                }
            }
        }
    }
}
