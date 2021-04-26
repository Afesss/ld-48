using UnityEngine;

[CreateAssetMenu(fileName = "CityEventSettings", menuName = "LD48/CityEvent")]
public class CityEventSettings : ScriptableObject
{
    /// <summary>
    /// Заголовок
    /// </summary>
    public string Title { get { return title; } }

    /// <summary>
    /// Тип события
    /// </summary>
    public CityEvents.Type Type { get { return type; } }

    /// <summary>
    /// Одноразовое событие
    /// </summary>
    public bool IsDisposable { get { return isDisposable; } }

    /// <summary>
    /// Бонус удовлетворения в процентах
    /// </summary>
    public float SatisfactionBonusRate { get { return satisfactionBonusRate; } }

    /// <summary>
    /// Стоимость
    /// </summary>
    public int Price { get { return price; } }

    [Tooltip("Заголовок")]
    [SerializeField]
    private string title;

    [Tooltip("Тип события")]
    [SerializeField]
    private CityEvents.Type type;

    [Tooltip("Одноразовое событие")]
    [SerializeField]
    private bool isDisposable;

    [Tooltip("Бонус удовлетворения в процентах (от 0 до 1)")]
    [Range(0f, 1f)]
    [SerializeField]
    private float satisfactionBonusRate;

    [Tooltip("Стоимость")]
    [SerializeField]
    private int price;
}