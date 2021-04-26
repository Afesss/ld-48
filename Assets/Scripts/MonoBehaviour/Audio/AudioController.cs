using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

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
        gameManager.OnChangeAudio += ChangeBackgroundAudio;
        audioSource.playOnAwake = false;
        audioSource.clip = audioSettings.mainMenu;
        audioSource.Play();
        currentGameAudioClip = audioSettings.easyGame;
    }
    private void ChangeBackgroundAudio(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.RUNNING:
                audioSource.clip = currentGameAudioClip;
                break;
            case GameManager.GameState.PAUSE:
                audioSource.clip = audioSettings.mainMenu;
                break;
            case GameManager.GameState.GAME_OVER:
                audioSource.clip = audioSettings.gameOver;
                break;
        }
        audioSource.Play();
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
        audioSource.clip = currentGameAudioClip;
        audioSource.Play();
    }
    private void OnDisable()
    {
        gameManager.OnChangeAudio -= ChangeBackgroundAudio;
    }
}
