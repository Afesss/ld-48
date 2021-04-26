using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RocketController : MonoBehaviour, IBuildController
{

    [SerializeField]
    private RocketView rocketView;

    [SerializeField]
    private ObjectSelectionController controller;

    [SerializeField]
    internal Transform spawnPoint;

    [SerializeField]
    internal Transform[] wayPoints;

    public bool CanBuild { get { return !isBuilt; } }

    public bool CanUpgrade { get { return false; } }

    public bool CanInteract { get { return isBuilt; } }

    public int BuildPrice { get { return settings.BuildPrice; } }
    public int UpgradePrice { get { return 0; } }
    public int InteractPrice { get { return settings.LaunchPrice; } }

    public string InteractTitle { get { return "Launch"; } }

    private bool isBuilt = false;

    private SignalBus signalBus;
    private Money money;
    private RocketSettings settings;
    private Dump dump;

    [Inject]
    private void Construct(SignalBus signalBus, Dump dump, Money money, RocketSettings settings)
    {
        this.signalBus = signalBus;
        this.money = money;
        this.settings = settings;
        this.dump = dump;
    }

    private void Awake()
    {
        if (controller != null)
            controller.OnObjectSelect += OnObjectSelect;
    }

    public void Build()
    {
        if (money.SubtractMoney(settings.BuildPrice))
        {
            isBuilt = true;
            rocketView.Build();
        }
    }

    public void Interact()
    {
        if (money.SubtractMoney(settings.BuildPrice))
        {
            dump.SendGarbage(dump.CurrentStorageGarbage);
            dump.SendTruck(spawnPoint, wayPoints);
        }
    }

    public void Upgrade(){}

    private void OnGarbageArrived()
    {
        rocketView.Launch();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnGarbageArrived();
    }

    private void OnObjectSelect()
    {
        signalBus.Fire(new SelectBuildObjectSignal() { Controller = this });
    }

    private void OnDestroy()
    {
        if (controller != null)
            controller.OnObjectSelect -= OnObjectSelect;
    }

}