using System;
using UnityEngine;

[Serializable]
public struct FactorySet
{
    public Factory.Type type;
    public Settings settings;

    [Serializable]
    public struct Settings
    {
        [Tooltip("Загрязнение воздуха")]
        public float AirPollutionPerSecond;

        [Tooltip("Загрязнение леса")]
        public float ForestPollutionPerSecond;

        [Tooltip("Загрязнение воды")]
        public float WaterPollutionPerSecond;

        [Tooltip("Вместимость хранилища завода")]
        public int StorageCapacity;

        [Tooltip("Количество запрашиваемого мусора со свалки")]
        public int GarbageAmountDemand;

        [Tooltip("Недовольства в секунду")]
        public float DissatisfiedAmountPerSecond;

        [Tooltip("Сжигания в секунду")]
        public float BurnAmountPerTick;

        [Tooltip("Стоимость постройкн")]
        public int BuildPrice;

        [Tooltip("Настройки улучшений")]
        public FactoryUpgradeSettings[] Upgrades;
    }
}