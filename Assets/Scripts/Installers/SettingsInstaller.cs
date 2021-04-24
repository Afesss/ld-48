﻿using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    public Money.Settings Money;
    public Dissatisfied.Settings Dissatisfied;
    //[Header("Настройки таймера")]
    //public SomeService.Settings Timer;

    //[Header("Настройки интерфейса")]
    //public UISettings UI;

    [Header("Настройки экологии")]
    public Ecology.Settings Ecology;

    [Serializable]
    public struct UISettings
    {
        //[Tooltip("Настройки отображения задачи")]
        //public UITaskView.Settings Task;

        //[Tooltip("Настройки отображения списка задач")]
        //public UITaskListView.Settings TaskList;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(Money);
        Container.BindInstance(Dissatisfied);
//        Container.BindInstance(Timer);
//        Container.BindInstance(UI.Task);
//        Container.BindInstance(UI.TaskList);
        Container.BindInstance(Ecology);
    }
}