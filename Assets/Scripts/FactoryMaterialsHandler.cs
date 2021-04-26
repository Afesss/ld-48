using UnityEngine;
using System;
using Zenject;

public class FactoryMaterialsHandler : IInitializable
{ 
    private Color currentWallCollor;
    private Color currentRoofColor;
    private Settings settings;
    
    public FactoryMaterialsHandler(Settings settings)
    {
        this.settings = settings;
    }
    public void Initialize()
    {
        ChangeFactoryMaterialColor(EcologyPollutionState.Minimum);
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
