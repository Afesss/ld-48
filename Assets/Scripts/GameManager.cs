using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : IInitializable
{
    private enum GameScene
    {
        MENU,
        GAME
    }
    internal enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSE
    }

    internal GameState currentGameState { get; private set; }
    private List<AsyncOperation> loadOperations = new List<AsyncOperation>();
   
    public void Initialize()
    {
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
        operation.completed += Operation_completed;
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
        operation.completed += Operation_completed;
    }
    private void Operation_completed(AsyncOperation operation)
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
            if (currentGameState == GameState.PAUSE)
            {
                UnloadScene(GameScene.GAME);
            }
            LoadScene(GameScene.GAME);
            UpdateGameState(GameState.RUNNING);
        }
    }

    internal void UpdateGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.PREGAME:
                break;
            case GameState.RUNNING:
                Time.timeScale = 1;
                break;
            case GameState.PAUSE:
                Time.timeScale = 0;
                break;
        }
        currentGameState = gameState;
    }

    
}
