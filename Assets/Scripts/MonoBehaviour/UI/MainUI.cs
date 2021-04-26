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
    [SerializeField] private ParticleSystem rain;
    
    private GameManager gameManager;
    #endregion

    #region Construct
    [Inject]
    public void Construct(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    #endregion

    #region Methods
    protected void Awake()
    {
        //DontDestroyOnLoad(this);
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
        gameManager.StartGame();
        
        mainEventSystem.SetActive(false);
        mainMenu.SetActive(false);
        StartCoroutine(CameraHid());
    }
    private IEnumerator CameraHid()
    {
        yield return new WaitForSeconds(0.1f);
        menuCamera.SetActive(false);
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
        rain.Play();
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
    public void Developers()
    {
        Application.OpenURL("https://vk.com/whitecubegames");
    }
    #endregion
}
