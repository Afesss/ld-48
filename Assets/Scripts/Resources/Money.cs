using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class Money
{
    #region Variables
    internal event Action OnMoneyAmountChange;

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
        OnMoneyAmountChange?.Invoke();
    }
    internal bool SubtractMonet(int amount)
    {
        if(moneyAmount < amount)
        {
            Debug.Log("Недостаточно денег");
            return false;

        }

        moneyAmount -= amount;
        OnMoneyAmountChange?.Invoke();
        Debug.Log(moneyAmount);
        return true;
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
