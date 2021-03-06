using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    [Header("Настройка материалов города")]
    public FactoryMaterialsHandler.Settings CityMaterials;
    [Header("Настройки аудио")]
    public AudioSettings Audio;
    [Header("Image UI Game Over")]
    public UIGameOver.Settings UIGameOver;
    [Header("Настройки пула грузовиков")]
    public TruckPool.Settings TruckPool;
    [Header("Настройки денег")]
    public Money.Settings Money;
    [Header("Настройки недовольства")]
    public Dissatisfied.Settings Dissatisfied;
    [Header("Настройки грузовичков")]
    public TruckSpawner.Settings CarSpawner;
    [Header("Настройки мусорнго полигона")]
    public Dump.Settings Dump;
    [Header("Настройки ракеты")]
    public RocketSettings Rocket;
    [Header("Настройки городских событий")]
    public CityEvents.Settings CityEvents;

    [Header("Настройки экологии")]
    public Ecology.Settings Ecology;

    [Header("Настройки заводов")]
    public FactorySet[] Factory;

    //[Serializable]
    //public struct UISettings
    //{
        //[Tooltip("Настройки отображения задачи")]
        //public UITaskView.Settings Task;

        //[Tooltip("Настройки отображения списка задач")]
        //public UITaskListView.Settings TaskList;
    //}

    public override void InstallBindings()
    {
        Container.BindInstance(Money);
        Container.BindInstance(Dissatisfied);
        Container.BindInstance(CarSpawner);
        Container.BindInstance(Dump);
        Container.BindInstance(Rocket);
        Container.BindInstance(TruckPool);
        Container.BindInstance(UIGameOver);
        Container.BindInstance(Audio);
        Container.BindInstance(CityMaterials);
//        Container.BindInstance(Timer);
//        Container.BindInstance(UI.Task);
//        Container.BindInstance(UI.TaskList);
        Container.BindInstance(Ecology);
        Container.BindInstance(Factory);
        Container.BindInstance(CityEvents);
    }
}