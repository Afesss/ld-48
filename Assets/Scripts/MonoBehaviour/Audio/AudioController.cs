using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
public static class AudioEventBroker
{
    public static event Action OnAudioEvent;
    public static void OnAudioEventInvoke()
    {
        OnAudioEvent?.Invoke();
    }
}
public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource mouseAudioSource;
    [SerializeField] private AudioSource truckAudioSource;
    [SerializeField] private AudioSource constructAudioSource;

    private float truckVolume = 0.1f;

    private EcologyPollutionState currentEcologyState;
    private AudioClip currentGameAudioClip;
    private AudioSettings audioSettings;
    private GameManager gameManager;
    private SignalBus signalBus;
    private Ecology ecology;
    [Inject]
    private void Construct(AudioSettings audioSettings,GameManager gameManager,Ecology ecology)
    {
        this.audioSettings = audioSettings;
        this.gameManager = gameManager;
        this.ecology = ecology;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        mouseAudioSource.playOnAwake = false;
        mouseAudioSource.loop = false;
        mouseAudioSource.clip = audioSettings.click;

        truckAudioSource.playOnAwake = false;
        truckAudioSource.loop = true;
        truckAudioSource.volume = truckVolume;
        truckAudioSource.clip = audioSettings.truckWheels;

        constructAudioSource.playOnAwake = false;
        constructAudioSource.loop = false;
        constructAudioSource.clip = audioSettings.construction;

        backgroundAudioSource.playOnAwake = false;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.clip = audioSettings.mainMenu;
        backgroundAudioSource.Play();

        currentGameAudioClip = audioSettings.easyGame;

        
    }
    
    private void Ecology_OnEcologyChange(Ecology.Type type)
    {
        if (gameManager.currentGameState == GameManager.GameState.GAME_OVER)
        {
            return;
        }
        float currentRate = ecology.GetCurrenMaxPollutionRate();
        if(currentRate > 0.3f)
        {
            if(currentEcologyState == EcologyPollutionState.Medium)
            {
                return;
            }
            currentEcologyState = EcologyPollutionState.Medium;
        }
        else if(currentRate > 0.6f)
        {
            if (currentEcologyState == EcologyPollutionState.Hard)
            {
                return;
            }
            currentEcologyState = EcologyPollutionState.Hard;
        }
        else
        {
            if (currentEcologyState == EcologyPollutionState.Minimum)
            {
                return;
            }
            currentEcologyState = EcologyPollutionState.Minimum;
        }
        ChangeGameAudioClip(currentEcologyState);
    }

    private void OnDestroy()
    {
        ecology.OnEcologyChange -= Ecology_OnEcologyChange;
        AudioEventBroker.OnAudioEvent -= PlayConstructAudio;
    }
    private void Start()
    {
        gameManager.OnChangeAudio += ChangeBackgroundAudio;
        ecology.OnEcologyChange += Ecology_OnEcologyChange;

        AudioEventBroker.OnAudioEvent += PlayConstructAudio;
    }
    private void PlayConstructAudio()
    {
        constructAudioSource.Play();
    }
    private void ChangeBackgroundAudio(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.RUNNING:
                backgroundAudioSource.clip = currentGameAudioClip;
                truckAudioSource.Play();
                break;
            case GameManager.GameState.PAUSE:
                backgroundAudioSource.clip = audioSettings.mainMenu;
                truckAudioSource.Stop();
                break;
            case GameManager.GameState.GAME_OVER:
                backgroundAudioSource.clip = audioSettings.gameOver;
                truckAudioSource.Stop();
                break;
        }
        backgroundAudioSource.Play();
    }
    private void ChangeGameAudioClip(EcologyPollutionState ecologyPollutionState)
    {
        switch (ecologyPollutionState)
        {
            case EcologyPollutionState.Minimum:
                currentGameAudioClip = audioSettings.easyGame;
                break;
            case EcologyPollutionState.Medium:
                currentGameAudioClip = audioSettings.mediumGame;
                break;
            case EcologyPollutionState.Hard:
                currentGameAudioClip = audioSettings.hardGame;
                break;
        }
        backgroundAudioSource.clip = currentGameAudioClip;
        backgroundAudioSource.Play();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mouseAudioSource.Play();
        }
    }
    private void OnDisable()
    {
        gameManager.OnChangeAudio -= ChangeBackgroundAudio;
    }
}
