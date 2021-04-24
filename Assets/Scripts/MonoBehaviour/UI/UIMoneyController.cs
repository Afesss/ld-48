using System;
using TMPro;
using UnityEngine;
using Zenject;

public class UIMoneyController : MonoBehaviour
{
    [Tooltip("Поле с количеством денег")]
    [SerializeField]
    private TextMeshProUGUI moneyAmountField;

    private Money money;

    [Inject]
    private void Construct(Money money)
    {
        this.money = money;
    }

    private void Awake()
    {
        if (money != null)
            money.OnMoneyAmountChange += OnMoneyAmountChange;
        moneyAmountField.text = money.moneyAmount.ToString();
    }

    private void OnMoneyAmountChange()
    {
        moneyAmountField.text = money.moneyAmount.ToString();
    }

    private void OnDestroy()
    {
        if (money != null)
            money.OnMoneyAmountChange -= OnMoneyAmountChange;
    }
}
