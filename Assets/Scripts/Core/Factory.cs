using System;
using UnityEngine;
using Zenject;

public class Factory
{
    public FactorySet.Settings Settings { get { return settings; } }

    private FactorySet.Settings settings;
    private Money money;
    private Dissatisfied dissatisfied;
    private Ecology ecology;
    private Type type;

    private int currentUpgrade = 0;

    private bool isBurning = false;

    private float currentAirPollutionPerSecond = 0;
    private float currentForestPollutionPerSecond = 0;
    private float currentWaterPollutionPerSecond = 0;
    private float currentStorageAmount = 0;

    public Factory(Type type, FactorySet[] settings, Money money, Dissatisfied dissatisfied, Ecology ecology)
    {
        this.settings = getSetingsByType(type, settings);
        this.money = money;
        this.dissatisfied = dissatisfied;
        this.ecology = ecology;
        this.type = type;

        currentAirPollutionPerSecond = this.settings.AirPollutionPerSecond;
        currentForestPollutionPerSecond = this.settings.ForestPollutionPerSecond;
        currentWaterPollutionPerSecond = this.settings.WaterPollutionPerSecond;
    }

    /// <summary>
    /// Проверяет есть ли свободное место на складе
    /// </summary>
    /// <returns>Есть ли место</returns>
    public bool IsStorageFull()
    {
        return currentStorageAmount >= settings.StorageCapacity;
    }

    /// <summary>
    /// Возвращает свободное место на складе
    /// </summary>
    /// <returns>Количество свободного места</returns>
    public float GetFreeStorageSpace()
    {
        return settings.StorageCapacity - currentStorageAmount;
    }


    public float GetStorageSpaceRate()
    {
        return currentStorageAmount / settings.StorageCapacity;
    }

    /// <summary>
    /// Добавляет на склад заданное количество мусора
    /// Если добавить не получилось возвращает false
    /// </summary>
    /// <param name="amount">Количество мусора</param>
    /// <returns>Получилось ли добавить мусор</returns>
    public bool AddGarbageToStorage(float amount)
    {
        if (amount > GetFreeStorageSpace())
            return false;

        currentStorageAmount += amount;
        if (currentStorageAmount > 0 && !isBurning)
            StartBurn();

        return true;
    }

    /// <summary>
    /// Запустить завод
    /// </summary>
    public void StartBurn()
    {
        isBurning = true;
    }

    /// <summary>
    /// Остановить завод
    /// </summary>
    public void StopBurn()
    {
        isBurning = false;
    }

    /// <summary>
    /// Усовершенствовать завод
    /// </summary>
    /// <returns>Получилось ли обновить завод</returns>
    public bool DoUpgrade()
    {
        if (CanUpgrade())
        {
            var upgradeSettings = settings.Upgrades[currentUpgrade];

            if (money.SubtractMonet(upgradeSettings.UpgradeCost))
            {
                currentAirPollutionPerSecond = upgradeSettings.AirPollutionDecreaseAmount;
                currentForestPollutionPerSecond = upgradeSettings.ForesPollutionDecreaseAmount;
                currentWaterPollutionPerSecond = upgradeSettings.WaterPollutionDecreaseAmount;

                currentUpgrade++;
                return true;
            }
        }

        return false;
    }

    public bool DoBuild()
    {
        if (money.SubtractMonet(settings.BuildPrice))
            return true;
        return false;
    }

    /// <summary>
    /// Возвращает стоимость следующего улучшения
    /// </summary>
    /// <returns>Стоимость следующего улучшения</returns>
    public int GetNextUpgradeCost()
    {
        if (CanUpgrade())
            return settings.Upgrades[currentUpgrade].UpgradeCost;
        return 0;
    }

    /// <summary>
    /// Проверяет можно ли улучшать текущий завод
    /// Если есть улучшения и хватает денег, то возвращает true
    /// </summary>
    /// <returns>Можно ли улучшить</returns>
    public bool CanUpgrade()
    {
        return currentUpgrade < settings.Upgrades.Length;
    }

    /// <summary>
    /// Жгем мусор каждый кадр
    /// </summary>
    public bool DoBurnJob()
    {
        if (isBurning)
        {
            Debug.Log(currentStorageAmount);
            currentStorageAmount -= settings.BurnAmountPerTick * Time.deltaTime;
            if (currentStorageAmount < 0)
                StopBurn();
            IncreasePollution();
            dissatisfied.AddDissatisfied(settings.DissatisfiedAmountPerSecond * Time.deltaTime);

            return true;
        }
        return false;
    }

    private void IncreasePollution()
    {
        switch (type)
        {
            case Type.Water:
                ecology.AddParameter(Ecology.Type.Water, currentWaterPollutionPerSecond);
                ecology.AddParameter(Ecology.Type.Forest, currentForestPollutionPerSecond);
                ecology.AddParameter(Ecology.Type.Air, currentAirPollutionPerSecond);
                break;
            case Type.Forest:
                ecology.AddParameter(Ecology.Type.Forest, currentForestPollutionPerSecond);
                ecology.AddParameter(Ecology.Type.Air, currentAirPollutionPerSecond);
                break;
            case Type.Air:
                ecology.AddParameter(Ecology.Type.Air, currentAirPollutionPerSecond);
                break;
        }
    }

    private FactorySet.Settings getSetingsByType(Type type, FactorySet[] set)
    {
        for(var i = 0; i < set.Length; i++)
            if (set[i].type == type)
                return set[i].settings;

        throw new ArgumentOutOfRangeException();
    }

    public enum Type
    {
        /// <summary>
        /// Загрязняет воздух
        /// </summary>
        Air,
        /// <summary>
        /// Загрязняет воздух и лес
        /// </summary>
        Forest,
        /// <summary>
        /// Загрязняет воздух и лес и вода
        /// </summary>
        Water,
    }
}