using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DumpController : MonoBehaviour
{
    [SerializeField]
    private UIStorageView storageView;

    private Dump dump;

    [Inject]
    private void Constructor(Dump dump)
    {
        this.dump = dump;
    }



}
