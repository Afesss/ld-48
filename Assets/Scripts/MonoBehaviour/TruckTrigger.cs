using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dump;
    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject == dump)
        {
            //Вызвать метод обработки;
        }
    }
}
