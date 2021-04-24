using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<SomeService>().AsSingle();
        //Container.BindInterfacesAndSelfTo<SomeService>().AsSingle();
        Container.BindInterfacesAndSelfTo<Ecology>().AsSingle();
    }
}