using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class Money
{
    #region Variables
    internal int moneyAmount { get; private set; }

    private Settings settings;
    #endregion

    #region Construct
    public Money(Settings settings)
    {
        this.settings = settings;
    }
    #endregion

    #region Methods
    internal void AddMoney(int amount)
    {
        moneyAmount += amount;
    }
    internal void SubtractMonet(int amount)
    {
        if(moneyAmount >= amount)
        {
            moneyAmount -= amount;
        }
        else
        {
            Debug.Log("Недостаточно денег");
        }
        Debug.Log(moneyAmount);
    }
    #endregion

    #region Struct
    [Serializable]
    public struct Settings
    {
        public int startAmount;
    }
    #endregion
}
