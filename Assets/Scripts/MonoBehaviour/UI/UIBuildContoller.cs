using UnityEngine;
using Zenject;

public class UIBuildContoller : MonoBehaviour
{
    #region Buttons

    [SerializeField]
    private UIActionView buyActionView;

    [SerializeField]
    private UIActionView upgradeActionView;

    [SerializeField]
    private UIActionView interactActionView;

    #endregion

    private SignalBus signalBus;

    [Inject]
    private void Constructor(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void Awake()
    {
        signalBus.Subscribe<SelectBuildObjectSignal>(OnBuildObjectSelect);
        signalBus.Subscribe<DeselectBuildObjectSignal>(OnDeselect);
        OnDeselect();
    }

    private void OnBuildObjectSelect(SelectBuildObjectSignal data)
    {
        UpdateUI(data);

        buyActionView.SetAction(()=> {
            data.Controller.Build();
            UpdateUI(data);
        });

        upgradeActionView.SetAction(() => {
            data.Controller.Upgrade();
            UpdateUI(data);
        });

        interactActionView.SetTitle(data.Controller.InteractTitle);
        interactActionView.SetAction(()=> {
            data.Controller.Interact();
            UpdateUI(data);
        });
    }

    private void UpdateUI(SelectBuildObjectSignal data)
    {
        buyActionView.SetActive(data.Controller.CanBuild);
        upgradeActionView.SetActive(data.Controller.CanUpgrade);
        interactActionView.SetActive(data.Controller.CanInteract);

        if (data.Controller.CanBuild)
        {
            buyActionView.SetPrice(data.Controller.BuildPrice);
        }

        if (data.Controller.CanUpgrade)
        {
            upgradeActionView.SetPrice(data.Controller.UpgradePrice);
        }

        if (data.Controller.CanInteract)
        {
            interactActionView.SetPrice(data.Controller.InteractPrice);
        }
    }

    private void OnDeselect()
    {
        buyActionView.SetActive(false);
        upgradeActionView.SetActive(false);
        interactActionView.SetActive(false);
    }

    private void OnDestroy()
    {
        signalBus.Unsubscribe<SelectBuildObjectSignal>(OnBuildObjectSelect);
        signalBus.Unsubscribe<DeselectBuildObjectSignal>(OnDeselect);
    }
}