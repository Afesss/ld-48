using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject menuCamera;
    [SerializeField] private AudioListener audioListener;
    private GameManager gameManager;
    private bool gameRunning = false;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        Debug.Log(gameManager);
        this.gameManager = gameManager;
    }
    
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

        audioListener.enabled = false;
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
}
