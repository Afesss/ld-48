using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class Dump
{
    internal event Action DumpGameOver;
    internal event Action OnGarbageAmountUpdate;

    #region Variables
    private float currentStorageGarbage = 0;
    private Settings settings;
    private TruckPool truckPool;
    private Money money;
    #endregion

    #region Construct
    public Dump(Settings settings, Money money,TruckPool truckPool)
    {
        this.truckPool = truckPool;
        this.settings = settings;
        this.money = money;
    }
    #endregion

    #region Methods
    internal void GetResources()
    {
        if (currentStorageGarbage > settings.maxStorageGarbageAmount)
        {
            DumpGameOver?.Invoke();
        }
        else
        {
            currentStorageGarbage += settings.deliveredGarbageByOneTruck;
            OnGarbageAmountUpdate?.Invoke();
        }

        money.AddMoney(settings.deliveredMoneyByOneTruck);
    }
    internal void SendGarbage(float amount)
    {
        if (currentStorageGarbage >= amount)
        {
            currentStorageGarbage -= amount;
            OnGarbageAmountUpdate?.Invoke();
        }
        else
        {
            //TODO: delete in production.
            Debug.Log("Недостаточной мусора для отправки");
        }
    }
    public void SendTruck(TruckRoute truckRoute)
    {
        TruckBehaviour car = truckPool.truckPollService.GetFreeElement();
        car.SetPath(truckRoute);
    }
    internal float GetGarbageAmountRate()
    {
        return currentStorageGarbage / settings.maxStorageGarbageAmount;
    }
    #endregion

    #region Struct
    [Serializable]
    public struct Settings
    {
        [Tooltip("Максимальное количество допустимого мусора на складе")]
        public float maxStorageGarbageAmount;
        [Tooltip("Количество доставляемого мусора одним грузовиком")]
        public float deliveredGarbageByOneTruck;
        [Tooltip("Количество доставляемых денег одним грузовиком")]
        public int deliveredMoneyByOneTruck;
    }
    #endregion
}
