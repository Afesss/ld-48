using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Money>().AsSingle();
        Container.BindInterfacesAndSelfTo<Dissatisfied>().AsSingle();
        Container.BindInterfacesAndSelfTo<CarSpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<Dump>().AsSingle();
        //Container.Bind<SomeService>().AsSingle();
        //Container.BindInterfacesAndSelfTo<SomeService>().AsSingle();
        Container.BindInterfacesAndSelfTo<Ecology>().AsSingle();
        Container.Bind<FactorySet>().AsSingle();
    }
}