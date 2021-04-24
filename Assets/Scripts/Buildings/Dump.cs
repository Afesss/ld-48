using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class Dump
{
    internal event Action DumpGameOver;

    #region Variables
    private float currentStorageGarbage = 0;
    private Settings settings;
    private Money money;
    #endregion

    #region Construct
    public Dump(Settings settings, Money money)
    {
        this.settings = settings;
        this.money = money;
    }
    #endregion

    #region Methods
    internal void GetResources()
    {
        if (currentStorageGarbage > settings.maxStorageGarbageAmount)
        {
            DumpGameOver();
        }
        else
        {
            currentStorageGarbage += settings.deliveredGarbageByOneTruck;
        }

        money.AddMoney(settings.deliveredMoneyByOneTruck);
        Debug.Log("*___*");
    }
    internal void SendGarbage(float amount)
    {
        if (currentStorageGarbage >= amount)
        {
            currentStorageGarbage -= amount;
        }
        else
        {
            Debug.Log("Недостаточной мусора для отправки");
        }
    }
    #endregion

    #region Struct
    [Serializable]
    public struct Settings
    {
        public float maxStorageGarbageAmount;
        public float deliveredGarbageByOneTruck;
        public int deliveredMoneyByOneTruck;
    }
    #endregion
}
