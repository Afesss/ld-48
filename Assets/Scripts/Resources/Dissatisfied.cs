using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class Dissatisfied
{
    internal event Action DissatisfiedGameOver;
    internal event Action OnSatisfactionChange;

    #region Variables
    private float currentDissatisfied, overallDissatisfied;
    private Settings settings;
    #endregion

    #region Construct
    public Dissatisfied(Settings settings)
    {
        this.settings = settings;
    }
    #endregion

    #region Methods
    
    internal float GetRate()
    {
        return currentDissatisfied / settings.maxDissatified;
    }

    internal void AddDissatisfied(float amount)
    {
        currentDissatisfied += amount;
        OnSatisfactionChange?.Invoke();
        CheckGameOver();
    }
    internal void SubtractDissatisfied(float amount)
    {
        if(currentDissatisfied < amount)
        {
            currentDissatisfied = 0;
        }
        else
        {
            currentDissatisfied -= amount;
        }
        OnSatisfactionChange?.Invoke();
    }

    internal void DecreaseByRate(float rate)
    {
        SubtractDissatisfied(settings.maxDissatified * rate);
    }

    internal void CheckGameOver()
    {
        if(currentDissatisfied > settings.maxDissatified)
        {
            DissatisfiedGameOver?.Invoke();
        }
    }
    #endregion

    #region Struct
    [Serializable]
    public struct Settings
    {
        public float maxDissatified;
    }
    #endregion
}
