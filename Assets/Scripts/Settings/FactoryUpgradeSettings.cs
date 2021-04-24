using UnityEngine;

[CreateAssetMenu(fileName = "FactoryUpgradeSettings", menuName = "LD48/FactoryUpgrade")]
public class FactoryUpgradeSettings : ScriptableObject
{

    /// <summary>
    /// Снижение загрязнения воздуха
    /// </summary>
    public float AirPollutionDecreaseAmount { get { return airPollutionDecreaseAmount; } }

    /// <summary>
    /// Снижение загрязнения леса
    /// </summary>
    public float ForesPollutionDecreaseAmount { get { return foresPollutionDecreaseAmount; } }

    /// <summary>
    /// Снижение загрязнения воздуха
    /// </summary>
    public float WaterPollutionDecreaseAmount { get { return waterPollutionDecreaseAmount; } }

    /// <summary>
    /// Цена улучшения
    /// </summary>
    public int UpgradeCost { get { return upgradeCost; } }

    [Tooltip("Снижение загрязнения воздуха")]
    [SerializeField]
    private float airPollutionDecreaseAmount;

    [Tooltip("Снижение загрязнения леса")]
    [SerializeField]
    private float foresPollutionDecreaseAmount;

    [Tooltip("Снижение загрязнения воздуха")]
    [SerializeField]
    private float waterPollutionDecreaseAmount;

    [Tooltip("Цена улучшения")]
    [SerializeField]
    private int upgradeCost;
}