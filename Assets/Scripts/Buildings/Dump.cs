using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Dump: ITickable
{
    internal event Action<UIGameOver.GameOverVersion> DumpGameOver;
    internal event Action OnGarbageAmountUpdate;

    public float CurrentStorageGarbage { get { return currentStorageGarbage; } }

    #region Variables
    private float currentStorageGarbage = 0;
    private Settings settings;
    private TruckPool truckPool;
    private Money money;

    //private int carQueue = 0;
    private float currentDelayCountdoun = 0;
    private Queue<TruckRoute> routeQueue;

    #endregion

    #region Construct
    public Dump(Settings settings, Money money,TruckPool truckPool)
    {
        this.truckPool = truckPool;
        this.settings = settings;
        this.money = money;

        routeQueue = new Queue<TruckRoute>();
    }
    #endregion

    #region Methods
    internal void GetResources()
    {
        if (currentStorageGarbage > settings.maxStorageGarbageAmount)
        {
            DumpGameOver?.Invoke(UIGameOver.GameOverVersion.Dump);
        }
        else
        {
            currentStorageGarbage += settings.deliveredGarbageByOneTruck;
            OnGarbageAmountUpdate?.Invoke();
        }

        money.AddMoney(settings.deliveredMoneyByOneTruck);
    }
    internal bool SendGarbage(float amount)
    {
        if (currentStorageGarbage >= amount)
        {
            currentStorageGarbage -= amount;
            OnGarbageAmountUpdate?.Invoke();
            return true;
        }
        return false;
    }

    public void SendTruckDemand(Transform spawnPoint, Transform[] moveWayPoints, bool withQueue = false)
    {
        var truckRoute = new TruckRoute(spawnPoint, moveWayPoints);

        if (withQueue)
        {
            routeQueue.Enqueue(truckRoute);
            currentDelayCountdoun += settings.sendTruckDelay;
        }
        else
        {
            SendTrackProcess(truckRoute);
        }
    }

    public void Tick()
    {
        if (currentDelayCountdoun > 0)
        {
            if (currentDelayCountdoun < routeQueue.Count * settings.sendTruckDelay)
                SendTrackProcess(routeQueue.Dequeue());

            currentDelayCountdoun -= Time.deltaTime;

            if (currentDelayCountdoun < 0)
                currentDelayCountdoun = 0;
        }
    }

    private void SendTrackProcess(TruckRoute truckRoute)
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
        [Tooltip("Время задержки перед отправлением грузовика со склада")]
        public float sendTruckDelay;
    }
    #endregion
}
