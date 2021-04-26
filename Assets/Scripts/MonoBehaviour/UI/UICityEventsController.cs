using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UICityEventsController : MonoBehaviour
{
    [SerializeField]
    private UICityEventsView view;

    private CityEvents cityEvents;

    [Inject]
    private void Constructor(CityEvents cityEvents)
    {
        this.cityEvents = cityEvents;
    }

    private void Awake()
    {
        if (view != null)
        {
            view.RedrawList(cityEvents.GetList());
            view.OnCityEventSelect += OnCityEventSelect;
        }

        
    }

    private void OnCityEventSelect(int index)
    {
        if (cityEvents.Apply(index))
        {
            view.RedrawList(cityEvents.GetList());
            view.Close();
        }
    }

    private void OnDestroy()
    {
        if (view != null)
            view.OnCityEventSelect -= OnCityEventSelect;
    }
}