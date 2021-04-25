using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryController : MonoBehaviour, IBuildController
{
    [SerializeField]
    private Factory.Type factoryType;

    [SerializeField]
    private FactoryView factoryView;

    [SerializeField]
    private ObjectSelectionController controller;

    [SerializeField]
    internal Transform spawnPoint;

    [SerializeField]
    internal Transform[] wayPoints;


    private int factoryUpgradeLevel = 0;
    private Factory factory;

    private SignalBus signalBus;
    private Dump dump;

    public bool CanBuild { get { return factoryUpgradeLevel == 0; } }

    public bool CanUpgrade { get { return (factory.CanUpgrade() && factoryUpgradeLevel > 0); } }

    public bool CanInteract { get { return factoryUpgradeLevel != 0; } }

    public int BuildPrice { get { return factory.Settings.BuildPrice; } }
    public int UpgradePrice { get { return factory.GetNextUpgradeCost(); } }
    public int InteractPrice { get { return 0; } }

    public string InteractTitle { get { return "Deliver"; } }

    [Inject]
    private void Construct(SignalBus signalBus, Dump dump, FactorySet[] settings, Money money, Dissatisfied dissatisfied, Ecology ecology)
    {
        this.signalBus = signalBus;
        this.dump = dump;
        factory = new Factory(factoryType, settings, money, dissatisfied, ecology);
    }

    private void Awake()
    {
        factoryView.UpdateStorage(0);
        if (controller != null)
            controller.OnObjectSelect += OnObjectSelect;
    }

    public void Update()
    {
        if (factory.DoBurnJob())
        {
            factoryView.UpdateStorage(factory.GetStorageSpaceRate());
        }
    }

    private void OnObjectSelect()
    {
        signalBus.Fire(new SelectBuildObjectSignal() { Controller = this });
    }

    public void Build()
    {
        if (factory.DoBuild())
        {
            factoryUpgradeLevel = 1;
            factoryView.Upgrade();
        }
    }

    public void Upgrade()
    {
        if (factory.DoUpgrade())
        {
            factoryUpgradeLevel++;
            factoryView.Upgrade();
        }
    }

    public void Interact()
    {
        if (dump.SendGarbage(factory.Settings.GarbageAmountDemand))
        {
            if (factory.GetFreeStorageSpace() > factory.Settings.GarbageAmountDemand)
                dump.SendTruck(spawnPoint, wayPoints);
        }
    }

    private void OnGarbageArrived()
    {
        if (factory.AddGarbageToStorage(factory.Settings.GarbageAmountDemand))
        {
            factoryView.UpdateStorage(factory.GetStorageSpaceRate());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnGarbageArrived();
    }

    private void OnDestroy()
    {
        if (controller != null)
            controller.OnObjectSelect -= OnObjectSelect;
    }
}