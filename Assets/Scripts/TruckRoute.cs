using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckRoute
{
    private Transform spawnPoint;
    private Transform[] moveWayPoints;

    private int currentPoint;
    public TruckRoute(Transform spawnPoint,Transform[] moveWayPoints)
    {
        this.spawnPoint = spawnPoint;
        this.moveWayPoints = moveWayPoints;
    }
    internal Vector3 GetSpawnPoint()
    {
        currentPoint = 0;
        return spawnPoint.position;
    }
    internal Vector3 GetCurrentWayPoint()
    {
        return moveWayPoints[currentPoint].position;
    }
    internal void ChangeWayPoint()
    {
        if (currentPoint < moveWayPoints.Length -1)
        {
            currentPoint++;
        }
    }
}
