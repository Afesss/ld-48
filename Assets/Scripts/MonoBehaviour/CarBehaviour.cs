using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour, IPoolObject
{
    #region Variables
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float moveSpeed;

    private bool grounded;
    private Vector3 targetPosition;
    private Vector3 moveVector;
    #endregion

    #region Methods
    public void ReturnToPool()
    {
        grounded = false;
        gameObject.SetActive(false);
    }
    internal void SetPath(Vector3 from, Vector3 to)
    {
        transform.position = from;
        targetPosition = to;
    }
    private void FixedUpdate()
    {
        if (grounded)
        {
            
            moveVector = targetPosition - transform.position;
            transform.LookAt(targetPosition);
            transform.position += moveVector.normalized * moveSpeed * Time.deltaTime;
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
