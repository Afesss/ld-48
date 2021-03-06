using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : IInitializable
{
    internal event Action OnMainMenu;
    internal event Action<GameState> OnChangeAudio;
    internal event Action OnGameOver;
    internal event Action OnResumeGame;
    private Ecology ecology;
    #region Enum
    private enum GameScene
    {
        MENU,
        GAME
    }
    internal enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSE,
        GAME_OVER
    }
    #endregion
    public GameManager(Ecology ecology)
    {
        this.ecology = ecology;
    }
    #region Variables
    internal GameState currentGameState { get; private set; }
    internal GameState previousGameState { get; private set; }
    private List<AsyncOperation> loadOperations = new List<AsyncOperation>();
    public bool SceneUnloaded { get; set; }
    public bool rockatGameOver { get; set; }
    #endregion

    #region Methods
    public void Initialize()
    {
        rockatGameOver = false;
        currentGameState = GameState.PREGAME;
    }
    private void LoadScene(GameScene gameScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync((int)gameScene, LoadSceneMode.Additive);
        if (operation == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + gameScene);
            return;
        }

        loadOperations.Add(operation);
        operation.completed += LoadOperation_completed;
    }
    private void UnloadScene(GameScene gameScene)
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync((int)gameScene);
        if (operation == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + gameScene);
            return;
        }

        loadOperations.Add(operation);
        operation.completed += LoadOperation_completed;
    }
    private void UnloadOperation_completed(AsyncOperation operation)
    {
        if (loadOperations.Contains(operation))
        {
            loadOperations.Remove(operation);
            SceneUnloaded = true;
        }
    }
    private void LoadOperation_completed(AsyncOperation operation)
    {
        if (loadOperations.Contains(operation))
        {
            loadOperations.Remove(operation);
        }
    }


    internal void StartGame()
    {
        if (currentGameState != GameState.RUNNING)
        {
            ecology.ResetVuluesArray();
            if (currentGameState == GameState.PAUSE || currentGameState == GameState.GAME_OVER)
            {
                UnloadScene(GameScene.GAME);
            }
            LoadScene(GameScene.GAME);
            UpdateGameState(GameState.RUNNING);
            
        }
    }
    internal void OnGameOverInvoke()
    {
        OnMainMenu?.Invoke();
    }
    internal void UpdateGameState(GameState gameState)
    {
        previousGameState = currentGameState;
        switch (gameState)
        {
            case GameState.PREGAME:
                break;
            case GameState.RUNNING:
                OnResumeGame?.Invoke();
                Time.timeScale = 1;
                break;
            case GameState.PAUSE:
                Time.timeScale = 0;
                break;
            case GameState.GAME_OVER:
                OnGameOver?.Invoke();
                break;
        }
        currentGameState = gameState;
        OnChangeAudio?.Invoke(currentGameState);
    }
    #endregion
}
