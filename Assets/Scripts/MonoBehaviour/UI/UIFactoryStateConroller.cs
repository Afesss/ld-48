using System.Collections;
using UnityEngine;
using Zenject;

public class UIFactoryStateConroller : MonoBehaviour
{
    [SerializeField]
    private GameObject notEnoughSpaceIndicator;

    [Tooltip("Время отображения предупреждения о нехватке денег")]
    [SerializeField]
    private float alertDuration = 2;

    [Tooltip("")]
    [SerializeField]
    private float alertActiveDuration = 0.3f;

    [Tooltip("")]
    [SerializeField]
    private float alertInactiveDuration = 0.1f;

    private SignalBus signalBus;

    private Coroutine notEnoughSpaceCoroutine;
    private float alertCountdown = 2;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void Awake()
    {
        notEnoughSpaceIndicator.SetActive(false);
        signalBus.Subscribe<NotEnoughGarbageSpaceSignal>(OnNotEnoughGarbageSpase);
    }

    private void OnNotEnoughGarbageSpase()
    {
        alertCountdown = alertDuration;
        if (notEnoughSpaceCoroutine != null)
            StopCoroutine(notEnoughSpaceCoroutine);
        notEnoughSpaceCoroutine = StartCoroutine(ShowAlert());
    }

    private IEnumerator ShowAlert()
    {
        while (alertCountdown > 0)
        {
            notEnoughSpaceIndicator.SetActive(!notEnoughSpaceIndicator.activeSelf);
            var delay = (notEnoughSpaceIndicator.activeSelf) ? alertActiveDuration : alertInactiveDuration;
            alertCountdown -= delay;
            yield return new WaitForSeconds(delay);
        }
        notEnoughSpaceIndicator.SetActive(false);
    }

    private void OnDestroy()
    {
        signalBus.Unsubscribe<NotEnoughGarbageSpaceSignal>(OnNotEnoughGarbageSpase);
    }
}