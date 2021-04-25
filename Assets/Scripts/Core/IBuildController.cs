public interface IBuildController
{
    public bool CanBuild { get; }
    public bool CanUpgrade { get; }
    public bool CanInteract { get; }

    public int BuildPrice { get; }
    public int UpgradePrice { get; }
    public int InteractPrice { get; }

    public string InteractTitle { get; }

    public void Build();

    public void Upgrade();

    public void Interact();
}
