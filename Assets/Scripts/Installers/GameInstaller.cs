using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Money>().AsSingle();
        Container.BindInterfacesAndSelfTo<Dissatisfied>().AsSingle();
        //Container.Bind<SomeService>().AsSingle();
        //Container.BindInterfacesAndSelfTo<SomeService>().AsSingle();
        Container.BindInterfacesAndSelfTo<Ecology>().AsSingle();
        Container.BindInterfacesTo<Factory>();
    }
}