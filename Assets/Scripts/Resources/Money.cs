using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class Money
{
    #region Variables
    internal event Action OnMoneyAmountChange;
    internal event Action OnNotEnoughMoney;

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
            OnNotEnoughMoney?.Invoke();
            return false;
        }

        moneyAmount -= amount;
        OnMoneyAmountChange?.Invoke();
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
