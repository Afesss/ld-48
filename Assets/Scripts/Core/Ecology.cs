using System;
using System.Linq;
using UnityEngine;

public class Ecology
{
    public delegate void PollutionExceededAction(Type type);
    public event PollutionExceededAction OnPollutionExceeded;

    private Settings settings;

    private float[] values;

    public Ecology(Settings settings)
    {
        this.settings = settings;
        var count = Enum.GetNames(typeof(Type)).Length;
        values = new float[count];
    }

    /// <summary>
    /// �������� ������� ���������� ���������� �����������.
    /// ������� �� ������������� ��������� �����������.
    /// </summary>
    /// <returns>������� �����������</returns>
    public float GetCurrenPollutionRate()
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
        if (values[(int)type] > GetMaxValue(type))
        {
            OnPollutionExceeded?.Invoke(type);
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