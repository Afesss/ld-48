using UnityEngine;
using Zenject;

public class UIParkController : MonoBehaviour
{
    [SerializeField]
    private GameObject park;

    private CityEvents cityEvents;

    [Inject]
    private void Construct(CityEvents cityEvents)
    {
        this.cityEvents = cityEvents;
    }

    private void Awake()
    {
        park.SetActive(false);
        if (cityEvents != null)
            cityEvents.OnActionApply += OnCityEvent;
    }

    private void OnCityEvent(CityEvents.Type type)
    {
        if (type == CityEvents.Type.Park)
        {
            park.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (cityEvents != null)
            cityEvents.OnActionApply -= OnCityEvent;
    }
}