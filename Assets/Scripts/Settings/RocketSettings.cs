using System;
using UnityEngine;

[Serializable]
public struct RocketSettings
{
    [Tooltip("Стоимость постройки")]
    public int BuildPrice;

    [Tooltip("Стоимость запуска")]
    public int LaunchPrice;
}