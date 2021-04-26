using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject menuCamera;
    [SerializeField] private GameObject mainEventSystem;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject epilouge;
    
    private GameManager gameManager;
    private SignalBus signalBus;
    #endregion

    #region Construct
    [Inject]
    public void Construct(GameManager gameManager,SignalBus signalBus)
    {
        this.gameManager = gameManager;
        this.signalBus = signalBus;
    }
    #endregion

    #region Methods
    protected void Awake()
    {
        epilouge.SetActive(false);
        tutorial.SetActive(false);
        gameManager.OnGameOver += Pause;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&
            gameManager.currentGameState != GameManager.GameState.GAME_OVER)
        {
            Pause();
        }
    }
    public void NewGame()
    {
        signalBus.Fire(new MainMenuSignal());

        gameManager.StartGame();

        mainEventSystem.SetActive(false);
        mainMenu.SetActive(false);
        StartCoroutine(CameraHid());
    }
    private IEnumerator CameraHid()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (gameManager.SceneUnloaded)
            {
                menuCamera.SetActive(false);
                gameManager.SceneUnloaded = false;
                StopAllCoroutines();
            }
        }
    }
    public void Ñontinue()
    {
        if (gameManager.currentGameState == GameManager.GameState.PAUSE && 
            gameManager.previousGameState != GameManager.GameState.GAME_OVER)
        {
            gameManager.UpdateGameState(GameManager.GameState.RUNNING);
            mainMenu.SetActive(false);
            menuCamera.SetActive(false);
        }
    }
    internal void Pause()
    {
        if (gameManager.rockatGameOver)
        {
            tutorial.SetActive(false);
            epilouge.SetActive(true);
        }
        StopAllCoroutines();
        menuCamera.SetActive(true);
        mainMenu.SetActive(true);
        gameManager.UpdateGameState(GameManager.GameState.PAUSE);
    }
    public void Exit()
    {
        gameManager.OnGameOver -= Pause;
        Application.Quit();
    }
    public void ShowTutorial()
    {
        tutorial.SetActive(true);
    }
    public void HidTutorial()
    {
        tutorial.SetActive(false);
    }
    public void Developers()
    {
        Application.OpenURL("https://vk.com/whitecubegames");
    }
    #endregion
}
