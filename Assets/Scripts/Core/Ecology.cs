using System;
using System.Linq;
using UnityEngine;

public class Ecology
{
    public event Action<UIGameOver.GameOverVersion> OnPollutionExceeded;
    public delegate void EcologyAction(Type type);
    public event EcologyAction OnEcologyChange;

    private Settings settings;

    private float[] values;

    public Ecology(Settings settings)
    {
        this.settings = settings;
        var count = Enum.GetNames(typeof(Type)).Length;
        values = new float[count];
    }

    public float GetRate(Type type)
    {
        return values[(int)type] / GetMaxValue(type);
    }

    /// <summary>
    /// �������� ������� ���������� ���������� �����������.
    /// ������� �� ������������� ��������� �����������.
    /// </summary>
    /// <returns>������� �����������</returns>
    public float GetCurrenMaxPollutionRate()
    {
        float maxValue = values.Max();
        int maxIndex = values.ToList().IndexOf(maxValue);

        return maxValue / GetMaxValue((Type)maxIndex);
    }

    /// <summary>
    /// ��������� ����������� � ���������� ���������
    /// </summary>
    /// <param name="type">��� �����������</param>
    /// <param name="value">���������� ������������ �����������</param>
    public void AddParameter(Type type, float value)
    {
        values[(int)type] += value;
        OnEcologyChange?.Invoke(type);
        if (values[(int)type] > GetMaxValue(type))
        {
            switch (type)
            {
                case Type.Air:
                    OnPollutionExceeded?.Invoke(UIGameOver.GameOverVersion.Air);
                    break;
                case Type.Forest:
                    OnPollutionExceeded?.Invoke(UIGameOver.GameOverVersion.Forest);
                    break;
                case Type.Water:
                    OnPollutionExceeded?.Invoke(UIGameOver.GameOverVersion.Water);
                    break;
            }
            // TODO: ������ ����� �������
            Debug.Log($"���������� ������������ ����������� �� ��������� {type}");
        }
    }

    /// <summary>
    /// ������� ����������� � ���������� ���������
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public void SubtractParameter(Type type, int value)
    {
        values[(int)type] -= value;
        if (values[(int)type] < 0)
            values[(int)type] = 0;
    }

    /// <summary>
    /// ����� ������������ �������� ���������� ���������
    /// </summary>
    /// <param name="type">��� �����������</param>
    /// <returns>������������ �����������</returns>
    public float GetMaxValue(Type type)
    {
        switch (type)
        {
            case Type.Air:
                return settings.MaxAirValue;
            case Type.Forest:
                return settings.MaxForestValue;
            case Type.Water:
                return settings.MaxWaterValue;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    [Serializable]
    public struct Settings
    {
        [Tooltip("����������� �������� ����������� �������")]
        public int MaxAirValue;

        [Tooltip("����������� �������� ����������� ����")]
        public int MaxForestValue;

        [Tooltip("����������� �������� ����������� ����")]
        public int MaxWaterValue;
    }

    public enum Type
    {
        /// <summary>
        /// ������
        /// </summary>
        Air,
        /// <summary>
        /// ���
        /// </summary>
        Forest,
        /// <summary>
        /// ����
        /// </summary>
        Water,
    }
}