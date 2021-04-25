using System;
using UnityEngine;
using Zenject;

public class UIEcologyController : MonoBehaviour
{
    [SerializeField]
    private UIEcologyView AirView;

    [SerializeField]
    private UIEcologyView ForesView;

    [SerializeField]
    private UIEcologyView WaterView;

    private Ecology ecology;

    [Inject]
    private void Constructor(Ecology ecology)
    {
        this.ecology = ecology;
    }

    private void Awake()
    {
        AirView.SetValue(0);
        ForesView.SetValue(0);
        WaterView.SetValue(0);

        if (ecology != null)
            ecology.OnEcologyChange += OnEcologyChange;
    }

    private void OnEcologyChange(Ecology.Type type)
    {
        switch (type)
        {
            case Ecology.Type.Air:
                AirView.SetValue(ecology.GetRate(type));
                break;
            case Ecology.Type.Forest:
                ForesView.SetValue(ecology.GetRate(type));
                break;
            case Ecology.Type.Water:
                WaterView.SetValue(ecology.GetRate(type));
                break;
        }
    }

    private void OnDestroy()
    {
        if (ecology != null)
            ecology.OnEcologyChange -= OnEcologyChange;
    }
}