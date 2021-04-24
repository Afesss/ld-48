using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class Dissatisfied
{
    internal event Action DissatisfiedGameOver;

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
    
    internal void AddDissatisfied(float amount)
    {
        currentDissatisfied += amount;
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
    }
    internal void CheckGameOver()
    {
        if(currentDissatisfied > settings.maxDissatified)
        {
            DissatisfiedGameOver();
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
