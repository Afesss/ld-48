using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class TruckPool
{
    public Settings settings { get; private set; }
    public TruckPool(Settings settings)
    {
        this.settings = settings;
    }
    public PoolingService<TruckBehaviour> truckPollService { get; set; }

    [Serializable]
    public struct Settings
    {
        public TruckBehaviour carPrefab;
        public int poolCarCount;
    }

}
