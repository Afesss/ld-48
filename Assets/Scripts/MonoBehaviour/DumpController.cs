using System;
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

    private void Awake()
    {
        storageView.SetValueRate(0);
        if (dump != null)
            dump.OnGarbageAmountUpdate += OnGarbageAmountUpdate;
    }

    private void OnGarbageAmountUpdate()
    {
        storageView.SetValueRate(dump.GetGarbageAmountRate());
    }

    private void OnDestroy()
    {
        if (dump != null)
            dump.OnGarbageAmountUpdate -= OnGarbageAmountUpdate;
    }
}