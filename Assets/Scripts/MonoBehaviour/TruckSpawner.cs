using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class TruckSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField] private TruckWayPoints truckWayPoints;

    private int currentSpawnCarAmount = 0;
    private float timeToSpawnCar;
    private TruckPool truckPool;
    private Settings settings;
    #endregion

    #region Construct
    [Inject]
    public void Construct(Settings settings,TruckPool truckPool)
    {
        this.settings = settings;
        this.truckPool = truckPool;
    }
    #endregion

    #region Methods
    private void Start()
    {
        timeToSpawnCar = settings.StartTimeToCarSpawn;
        StartCoroutine(CarsSpawner());
    }
    private IEnumerator CarsSpawner()
    {
        TruckBehaviour car = truckPool.truckPollService.GetFreeElement();
        car.SetPath(new TruckRoute(truckWayPoints.SpawnPoint,truckWayPoints.WayPoints));

        currentSpawnCarAmount++;
        if(currentSpawnCarAmount > settings.countCarToChangeTimeSpawn)
        {
            if (timeToSpawnCar <= settings.minTimeToSpawnCar)
            {
                timeToSpawnCar = settings.minTimeToSpawnCar;
            }
            else
            {
                timeToSpawnCar -= settings.decreaseStepTimeToCarSpawn;
            }
            currentSpawnCarAmount = 0;
        }
        yield return new WaitForSeconds(timeToSpawnCar);

        StartCoroutine(CarsSpawner());
    }
    #endregion

    #region Struct
    [Serializable]
    public struct Settings
    {
        [Tooltip("Стартовое время до спавна следующей машины")]
        [Range(0,20)] public float StartTimeToCarSpawn;
        [Tooltip("Шаг уменьшения времени спавна")]
        [Range(0,5)] public float decreaseStepTimeToCarSpawn;
        [Tooltip("Минимальные время спавна")]
        [Range(0, 1)] public float minTimeToSpawnCar;
        [Tooltip("Через сколько машин уменьшается время спавна")]
        [Range(0,20)] public int countCarToChangeTimeSpawn;
    }
    #endregion
}
