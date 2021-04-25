using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct TruckSettings
{
    public float moveSpeed;
    public float rotationLerpSpeed;
    [Tooltip("Минимальная дистанция для смены WayPoint")]
    public float minDistanceToTarget;
}
