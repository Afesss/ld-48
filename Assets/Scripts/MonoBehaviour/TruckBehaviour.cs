using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBehaviour : MonoBehaviour, IPoolObject
{
    #region Variables
    [SerializeField] private Rigidbody _rigidbody;
    [Range(0,100)][SerializeField] private float moveSpeed;
    [Range(0,1)][SerializeField] private float rotationSlerpSpeed;
    [Range(0,5)][SerializeField] private float minDistanceToTarget;

    private bool grounded;
    private Vector3 targetPosition;
    private Vector3 moveVector;
    private TruckRoute route;
    #endregion

    #region Methods
    public void ReturnToPool()
    {
        grounded = false;
        gameObject.SetActive(false);
    }
    internal void SetPath(TruckRoute route)
    {
        transform.position = route.GetSpawnPoint();
        transform.LookAt(route.GetCurrentWayPoint());
        this.route = route;
    }
    private void FixedUpdate()
    {
        if (grounded)
        {
            targetPosition = route.GetCurrentWayPoint();
            moveVector = targetPosition - transform.position;
            if(moveVector.sqrMagnitude < minDistanceToTarget * minDistanceToTarget)
            {
                route.ChangeWayPoint();
            }
            Quaternion lookAtTarget = Quaternion.LookRotation(moveVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtTarget, rotationSlerpSpeed);
            //transform.Translate(0, 0, moveSpeed * Time.deltaTime);
            _rigidbody.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ReturnToPool();
    }
    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
    
    #endregion
}
