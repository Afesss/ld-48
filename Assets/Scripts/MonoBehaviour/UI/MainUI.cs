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
    [SerializeField] private GameObject hidOnStart;
    private GameManager gameManager;
    private bool gameRunning = false;
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
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }
    public void NewGame()
    {
        gameManager.StartGame();
        gameRunning = true;
        
        hidOnStart.SetActive(false);
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
        if (gameRunning)
        {
            gameManager.UpdateGameState(GameManager.GameState.RUNNING);
            mainMenu.SetActive(false);
            menuCamera.SetActive(false);
        }
    }
    internal void Pause()
    {
        StopAllCoroutines();
        menuCamera.SetActive(true);
        mainMenu.SetActive(true);

        gameManager.UpdateGameState(GameManager.GameState.PAUSE);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        gameRunning = false;
    }
    public void Developers()
    {
        Application.OpenURL("https://vk.com/whitecubegames");
    }
    #endregion
}
