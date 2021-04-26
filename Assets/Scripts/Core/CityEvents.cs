using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CityEvents
{
    public delegate void CityEventAction(Type type);
    public event CityEventAction OnActionApply;

    private Settings settings;
    private Money money;
    private Dissatisfied dissatisfied;

    private List<int> spentIndexes;

    public CityEvents(Settings settings, Money money, Dissatisfied dissatisfied)
    {
        this.settings = settings;
        this.money = money;
        this.dissatisfied = dissatisfied;
        spentIndexes = new List<int>();
    }

    public bool Apply(int index)
    {
        var set = Get(index);
        Debug.Log($"Try to do city event: {set.Title}");
        if (money.Spend(set.Price))
        {
            dissatisfied.DecreaseByRate(set.SatisfactionBonusRate);
            if (set.IsDisposable)
                spentIndexes.Add(index);
            OnActionApply?.Invoke(set.Type);
            return true;
        }
        return false;
    }

    public CityEventSettings[] GetList()
    {
        var result = new CityEventSettings[settings.Events.Length];
        for (var i = 0; i < settings.Events.Length; i++)
            result[i] = (!spentIndexes.Contains(i)) ? settings.Events[i] : null;
        return result;
    }

    public CityEventSettings Get(int index)
    {
        return settings.Events[index];
    }

    public enum Type
    {
        Simple,
        Park,
        Fireworks,
    }

    [Serializable]
    public struct Settings
    {
        [Tooltip("События")]
        public CityEventSettings[] Events;
    }
}