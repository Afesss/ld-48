using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckWayPoints : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] wayPoints;
    internal Transform SpawnPoint { get { return spawnPoint; } }
    internal Transform[] WayPoints { get { return wayPoints; } }
}
