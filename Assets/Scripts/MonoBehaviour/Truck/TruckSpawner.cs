using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class TruckSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField] private TruckWayPoints truckWayPoints;
    [SerializeField] private Transform pool;

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
        truckPool.truckPollService = new PoolingService<TruckBehaviour>(truckPool.settings.turckPrefab, 
            truckPool.settings.poolTruckCount, pool, true);
        StartCoroutine(CarsSpawner());
    }
    private IEnumerator CarsSpawner()
    {
        
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

        TruckBehaviour car = truckPool.truckPollService.GetFreeElement();
        car.SetPath(new TruckRoute(truckWayPoints.SpawnPoint, truckWayPoints.WayPoints));

        currentSpawnCarAmount++;
        yield return new WaitForSeconds(timeToSpawnCar);

        StartCoroutine(CarsSpawner());
    }
    #endregion

    #region Struct
    [Serializable]
    public struct Settings
    {
        [Tooltip("��������� ����� �� ������ ��������� ������")]
        [Range(0,20)] public float StartTimeToCarSpawn;
        [Tooltip("��� ���������� ������� ������")]
        [Range(0,5)] public float decreaseStepTimeToCarSpawn;
        [Tooltip("����������� ����� ������")]
        [Range(0, 1)] public float minTimeToSpawnCar;
        [Tooltip("����� ������� ����� ����������� ����� ������")]
        [Range(0,20)] public int countCarToChangeTimeSpawn;
    }
    #endregion
}
