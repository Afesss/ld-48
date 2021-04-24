using UnityEngine;

[CreateAssetMenu(fileName = "FactoryUpgradeSettings", menuName = "LD48/FactoryUpgrade")]
public class FactoryUpgradeSettings : ScriptableObject
{

    /// <summary>
    /// �������� ����������� �������
    /// </summary>
    public float AirPollutionDecreaseAmount { get { return airPollutionDecreaseAmount; } }

    /// <summary>
    /// �������� ����������� ����
    /// </summary>
    public float ForesPollutionDecreaseAmount { get { return foresPollutionDecreaseAmount; } }

    /// <summary>
    /// �������� ����������� �������
    /// </summary>
    public float WaterPollutionDecreaseAmount { get { return waterPollutionDecreaseAmount; } }

    /// <summary>
    /// ���� ���������
    /// </summary>
    public int UpgradeCost { get { return upgradeCost; } }

    [Tooltip("�������� ����������� �������")]
    [SerializeField]
    private float airPollutionDecreaseAmount;

    [Tooltip("�������� ����������� ����")]
    [SerializeField]
    private float foresPollutionDecreaseAmount;

    [Tooltip("�������� ����������� �������")]
    [SerializeField]
    private float waterPollutionDecreaseAmount;

    [Tooltip("���� ���������")]
    [SerializeField]
    private int upgradeCost;
}