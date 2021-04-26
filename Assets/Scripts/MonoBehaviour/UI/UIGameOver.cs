using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;
using System.Collections;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private Image gameOverImage;
    [SerializeField] private GameObject gameOverMenu;
    private Sprite currentSprite;
    private Settings settings;
    private GameManager gameManager;

    private Dump dump;
    private Ecology ecology;
    private Dissatisfied dissatisfied;
    private SignalBus signalBus;
    private FactoryMaterialsHandler factoryMaterials;
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
    public void Constuct(Settings settings, GameManager gameManager,Dump dump,Ecology ecology,
        Dissatisfied dissatisfied,SignalBus signalBus, FactoryMaterialsHandler factoryMaterials)
    {
        this.settings = settings;
        this.gameManager = gameManager;

        this.factoryMaterials = factoryMaterials;

        this.dump = dump;
        this.ecology = ecology;
        this.dissatisfied = dissatisfied;
        this.signalBus = signalBus;
    }
    private void Start()
    {
        gameOverMenu.SetActive(false);

        dump.DumpGameOver += EnableGameOver;
        ecology.OnPollutionExceeded += EnableGameOver;
        dissatisfied.DissatisfiedGameOver += EnableGameOver;

        signalBus.Subscribe<RocketSignal>(EnableGameOver);
    }
   
    private void OnDestroy()
    {
        dump.DumpGameOver -= EnableGameOver;
        ecology.OnPollutionExceeded -= EnableGameOver;
        dissatisfied.DissatisfiedGameOver -= EnableGameOver;
        signalBus.Unsubscribe<RocketSignal>(EnableGameOver);
    }
    private void EnableGameOver(RocketSignal rocketSignal)
    {
        EnableGameOver(rocketSignal.gameOverVersion);
    }
    public void EnableGameOver(GameOverVersion version)
    {
        if(gameManager.currentGameState == GameManager.GameState.GAME_OVER ||
            gameManager.previousGameState == GameManager.GameState.GAME_OVER)
        {
            return;
        }

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
                gameManager.rockatGameOver = true;
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
        factoryMaterials.ChangeFactoryMaterialColor(EcologyPollutionState.Minimum);
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
