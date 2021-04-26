using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Money>().AsSingle();
        Container.BindInterfacesAndSelfTo<Dissatisfied>().AsSingle();
        Container.BindInterfacesAndSelfTo<TruckSpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<Dump>().AsSingle();
        Container.BindInterfacesAndSelfTo<RocketBehaviour>().AsSingle();
        Container.BindInterfacesAndSelfTo<TruckPool>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIGameOver>().AsSingle();
        Container.BindInterfacesAndSelfTo<FactoryMaterialsHandler>().AsSingle();

        Container.DeclareSignal<RocketSignal>();
        //Container.Bind<SomeService>().AsSingle();
        //Container.BindInterfacesAndSelfTo<SomeService>().AsSingle();
        Container.BindInterfacesAndSelfTo<Ecology>().AsSingle();
        Container.BindInterfacesAndSelfTo<CityEvents>().AsSingle();
        Container.Bind<FactorySet>().AsSingle();

        
        Container.DeclareSignal<SelectBuildObjectSignal>();
        Container.DeclareSignal<DeselectBuildObjectSignal>();

        Container.DeclareSignal<AudioSignal>();
        Container.DeclareSignal<NotEnoughGarbageSpaceSignal>();
    }
}