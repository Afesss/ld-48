using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private Image gameOverImage;
    [SerializeField] private GameObject gameOverMenu;

    private Settings settings;
    private GameManager gameManager;
    private Sprite currentSprite;

    public enum GameOverVersion
    {
        Dump,
        Air,
        Forest,
        Water,
        Rocket,
        Dissatisfied
    }

    [Inject]
    public void Constuct(Settings settings, GameManager gameManager)
    {
        this.settings = settings;
        this.gameManager = gameManager;
    }
    private void Start()
    {
        gameOverMenu.SetActive(false);
    }
    public void EnableGameOvetr(GameOverVersion version)
    {
        switch (version)
        {
            case GameOverVersion.Dump:
                currentSprite = settings.dumpGameOver;
                break;
            case GameOverVersion.Air:
                currentSprite = settings.airGameOver;
                break;
            case GameOverVersion.Forest:
                currentSprite = settings.forestGameOver;
                break;
            case GameOverVersion.Water:
                currentSprite = settings.waterGameOver;
                break;
            case GameOverVersion.Rocket:
                currentSprite = settings.rocketGameOver;
                break;
            case GameOverVersion.Dissatisfied:
                currentSprite = settings.dissatisfiedGameOver;
                break;
            default:
                return;
        }
        gameOverImage.sprite = currentSprite;
        gameOverMenu.SetActive(true);
        gameManager.UpdateGameState(GameManager.GameState.GAME_OVER);
    }
    public void ReturnToMainMenu()
    {
        gameOverMenu.SetActive(false);
        gameManager.OnGameOverInvoke();
        
    }
    [Serializable]
    public struct Settings
    {
        public Sprite dumpGameOver;
        public Sprite airGameOver;
        public Sprite forestGameOver;
        public Sprite waterGameOver;
        public Sprite rocketGameOver;
        public Sprite dissatisfiedGameOver;
    }
}
