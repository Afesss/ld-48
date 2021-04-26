using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
public static class AudioEventBroker
{
    public static event Action OnAudioEvent;
    public static event Action OnResetAudio;
    public static void OnAudioEventInvoke()
    {
        OnAudioEvent?.Invoke();
    }
    public static void OnResetAudioInvoke()
    {
        OnResetAudio?.Invoke();
    }
}
public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource mouseAudioSource;
    [SerializeField] private AudioSource truckAudioSource;
    [SerializeField] private AudioSource constructAudioSource;

    private float truckVolume = 0.1f;

    private AudioClip currentGameAudioClip;
    private AudioSettings audioSettings;
    private GameManager gameManager;
    [Inject]
    private void Construct(AudioSettings audioSettings,GameManager gameManager)
    {
        this.audioSettings = audioSettings;
        this.gameManager = gameManager;
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
    
    private void OnDestroy()
    {
        AudioEventBroker.OnAudioEvent -= PlayConstructAudio;
        AudioEventBroker.OnResetAudio -= OnResetAudio;
    }
    private void Start()
    {
        gameManager.OnChangeAudio += ChangeBackgroundAudio;

        AudioEventBroker.OnAudioEvent += PlayConstructAudio;
        AudioEventBroker.OnResetAudio += OnResetAudio;
    }

    private void OnResetAudio()
    {
        backgroundAudioSource.Stop();
        backgroundAudioSource.clip = audioSettings.mainMenu;
        backgroundAudioSource.Play();
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
