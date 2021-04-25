using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class TruckPool : IInitializable
{
    private Settings settings;
    public TruckPool(Settings settings)
    {
        this.settings = settings;
    }
    public PoolingService<TruckBehaviour> truckPollService { get; private set; }

    public void Initialize()
    {
        truckPollService = new PoolingService<TruckBehaviour>(settings.carPrefab, settings.poolCarCount,new GameObject("Pool").transform , true);
    }
    [Serializable]
    public struct Settings
    {
        public TruckBehaviour carPrefab;
        public int poolCarCount;
    }

}
