using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<AudioSignal>();
        Container.DeclareSignal<MainMenuSignal>();

        Container.BindInterfacesAndSelfTo<Ecology>().AsSingle();
    }
}