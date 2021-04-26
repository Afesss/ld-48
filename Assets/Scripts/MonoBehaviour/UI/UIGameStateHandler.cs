using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIGameStateHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject hud;

    private GameManager gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Awake()
    {
        if (gameManager != null)
        {
            gameManager.OnGameOver += OnGameOver;
            gameManager.OnResumeGame += OnResumeGame;
        }
    }

    private void OnResumeGame()
    {
        hud.SetActive(true);
    }

    private void OnGameOver()
    {
        hud.SetActive(false);
    }

    private void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.OnGameOver -= OnGameOver;
            gameManager.OnResumeGame -= OnResumeGame;
        }
    }
}
