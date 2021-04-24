using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class CarSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField] private CarBehaviour carPrefab;
    [SerializeField] private Transform target;
    [SerializeField] private int poolCarCount;

    private PoolingService<CarBehaviour> carPollService = null;

    private int currentSpawnCarAmount = 0;
    private float timeToSpawnCar;
    
    private Settings settings;
    #endregion

    #region Construct
    [Inject]
    public void Construct(Settings settings)
    {
        this.settings = settings;
    }
    #endregion

    #region Methods
    private void Start()
    {
        if (carPrefab != null)
        {
            carPollService = new PoolingService<CarBehaviour>(carPrefab, poolCarCount, transform, true);
        }
        timeToSpawnCar = settings.StartTimeToCarSpawn;
        StartCoroutine(CarsSpawner());
    }
    private IEnumerator CarsSpawner()
    {
        CarBehaviour car = carPollService.GetFreeElement();
        car.SetPath(transform.position,target.position);

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
