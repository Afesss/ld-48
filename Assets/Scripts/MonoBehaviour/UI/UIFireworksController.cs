using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIFireworksController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;

    private CityEvents cityEvents;

    [Inject]
    private void Construct(CityEvents cityEvents)
    {
        this.cityEvents = cityEvents;
    }

    private void Awake()
    {
        if (cityEvents != null)
            cityEvents.OnActionApply += OnCityEvent;
    }

    private void OnCityEvent(CityEvents.Type type)
    {
        if (type == CityEvents.Type.Fireworks)
        {
            particle.Play();
        }
    }

    private void OnDestroy()
    {
        if (cityEvents != null)
            cityEvents.OnActionApply -= OnCityEvent;
    }
}
