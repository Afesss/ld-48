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
    /// Получить текущий процентный показатель загрязнения.
    /// Берется по максимальному параметру загрязнения.
    /// </summary>
    /// <returns>Процент загрязнения</returns>
    public float GetCurrenPollutionRate()
    {
        float maxValue = values.Max();
        int maxIndex = values.ToList().IndexOf(maxValue);

        return maxValue / GetMaxValue((Type)maxIndex);
    }

    /// <summary>
    /// Добавляет загрязнения к указанному параметру
    /// </summary>
    /// <param name="type">Тип загрязнения</param>
    /// <param name="value">Количество добавляемого загрязнения</param>
    public void AddParameter(Type type, float value)
    {
        values[(int)type] += value;
        if (values[(int)type] > GetMaxValue(type))
        {
            OnPollutionExceeded?.Invoke(type);
            Debug.Log($"Достигнуто максимальное загрязнение по параметру {type}");
        }
    }

    /// <summary>
    /// Убирает загрязнение у указанного параметра
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
    /// Берет максимальное значение указанного параметра
    /// </summary>
    /// <param name="type">Тип загрязнения</param>
    /// <returns>Максимальное загрязнение</returns>
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
        [Tooltip("Максимально значение загрязнения воздуха")]
        public int MaxAirValue;

        [Tooltip("Максимально значение загрязнения леса")]
        public int MaxForestValue;

        [Tooltip("Максимально значение загрязнения воды")]
        public int MaxWaterValue;
    }

    public enum Type
    {
        /// <summary>
        /// Воздух
        /// </summary>
        Air,
        /// <summary>
        /// Лес
        /// </summary>
        Forest,
        /// <summary>
        /// Вода
        /// </summary>
        Water,
    }
}