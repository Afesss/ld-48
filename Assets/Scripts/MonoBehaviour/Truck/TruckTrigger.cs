using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TruckTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dumpObject;
    private Dump dump;

    [Inject]
    public void Constract(Dump dump)
    {
        this.dump = dump;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject == dumpObject)
        {
            dump.GetResources();
        }
    }
}
