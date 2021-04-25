using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class UIMoneyController : MonoBehaviour
{
    [Tooltip("Поле с количеством денег")]
    [SerializeField]
    private TextMeshProUGUI moneyAmountField;

    [Tooltip("Предупреждение о нехватки денег")]
    [SerializeField]
    private GameObject notEnoughMoneyIndicator;

    [Tooltip("Время отображения предупреждения о нехватке денег")]
    [SerializeField]
    private float alertDuration = 2;

    [Tooltip("")]
    [SerializeField]
    private float alertActiveDuration = 0.3f;

    [Tooltip("")]
    [SerializeField]
    private float alertInactiveDuration = 0.1f;

    private Money money;
    private Coroutine notEnoughMoneyCoroutine;
    private float alertCountdown = 2;

    [Inject]
    private void Construct(Money money)
    {
        this.money = money;
    }

    private void Awake()
    {
        notEnoughMoneyIndicator.SetActive(false);
        if (money != null)
        {
            money.OnMoneyAmountChange += OnMoneyAmountChange;
            money.OnNotEnoughMoney += OnNotEnoughMoney;
        }
        moneyAmountField.text = money.moneyAmount.ToString();
    }

    private void OnNotEnoughMoney()
    {
        alertCountdown = alertDuration;
        if (notEnoughMoneyCoroutine != null)
            StopCoroutine(notEnoughMoneyCoroutine);
        notEnoughMoneyCoroutine = StartCoroutine(ShowAlert());
    }

    private void OnMoneyAmountChange()
    {
        moneyAmountField.text = money.moneyAmount.ToString();
    }

    private IEnumerator ShowAlert()
    {
        while (alertCountdown > 0)
        {
            notEnoughMoneyIndicator.SetActive(!notEnoughMoneyIndicator.activeSelf);
            var delay = (notEnoughMoneyIndicator.activeSelf) ? alertActiveDuration : alertInactiveDuration;
            alertCountdown -= delay;
            yield return new WaitForSeconds(delay);
        }
        notEnoughMoneyIndicator.SetActive(false);
    }

    private void OnDestroy()
    {
        if (money != null)
        {
            money.OnMoneyAmountChange -= OnMoneyAmountChange;
            money.OnNotEnoughMoney -= OnNotEnoughMoney;
        }
    }
}
