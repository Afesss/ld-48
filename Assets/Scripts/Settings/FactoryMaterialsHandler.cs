using UnityEngine;
using System;
using Zenject;

public class FactoryMaterialsHandler : IInitializable
{ 
    private Color currentWallCollor;
    private Color currentRoofColor;
    private Settings settings;
    private Ecology ecology;
    private SignalBus signalBus;
    public FactoryMaterialsHandler(Settings settings, Ecology ecology,SignalBus signalBus)
    {
        this.settings = settings;
        this.ecology = ecology;
        this.signalBus = signalBus;
    }
    public void Initialize()
    {
        ChangeFactoryMaterialColor(EcologyPollutionState.Minimum);
        ecology.OnEcologyChange += Ecology_OnEcologyChange;
        signalBus.Subscribe<MainMenuSignal>(ResetFactoryColor);
    }
    private void ResetFactoryColor()
    {
        ChangeFactoryMaterialColor(EcologyPollutionState.Minimum);
    }
    public void UnsubscribeEcologyEvent()
    {
        ecology.OnEcologyChange -= Ecology_OnEcologyChange;
    }
    private void Ecology_OnEcologyChange(Ecology.Type type)
    {
        float currentRate = ecology.GetCurrenMaxPollutionRate();
        if (currentRate > 0.3f)
        {
            ChangeFactoryMaterialColor(EcologyPollutionState.Medium);
        }
        else if(currentRate > 0.6f)
        {
            ChangeFactoryMaterialColor(EcologyPollutionState.Hard);
        }
    }

    public void ChangeFactoryMaterialColor(EcologyPollutionState ecologyPollution)
    {
        switch (ecologyPollution)
        {
            case EcologyPollutionState.Minimum:
                currentWallCollor = settings.minFacotryWallColor;
                currentRoofColor = settings.minFactoryRoofColor;
                break;
            case EcologyPollutionState.Medium:
                currentWallCollor = settings.midFactoryWallColor;
                currentRoofColor = settings.midFactoryRoofColor;
                break;
            case EcologyPollutionState.Hard:
                currentWallCollor = settings.hardFactoryWallColor;
                currentRoofColor = settings.hardFactoryRoofColor;
                break;
            default:
                return;
        }
        settings.factoryWall.color = currentWallCollor;
        settings.factoryRoof.color = currentRoofColor;
    }

    

    [Serializable]
    public struct Settings
    {
        [Header("Factory Wall")]
        public Material factoryWall;
        public Color minFacotryWallColor;
        public Color midFactoryWallColor;
        public Color hardFactoryWallColor;

        [Header("Factory Roof")]
        public Material factoryRoof;
        public Color minFactoryRoofColor;
        public Color midFactoryRoofColor;
        public Color hardFactoryRoofColor;
    }
}
