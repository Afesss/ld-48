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
    private GameManager gameManager;
    private bool isActive = true;
    #endregion

    #region Construct
    [Inject]
    public void Construct(Settings settings,TruckPool truckPool, GameManager gameManager)
    {
        this.settings = settings;
        this.truckPool = truckPool;
        this.gameManager = gameManager;
    }
    #endregion

    #region Methods
    private void Awake()
    {
        if (gameManager != null)
            gameManager.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        isActive = false;
    }

    private void Start()
    {
        isActive = true;
        timeToSpawnCar = settings.StartTimeToCarSpawn;
        truckPool.truckPollService = new PoolingService<TruckBehaviour>(truckPool.settings.turckPrefab, 
            truckPool.settings.poolTruckCount, pool, true);
        StartCoroutine(CarsSpawner());
    }
    private IEnumerator CarsSpawner()
    {
        while (isActive)
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
        }
    }

    private void OnDestroy()
    {
        if (gameManager != null)
            gameManager.OnGameOver -= OnGameOver;
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
