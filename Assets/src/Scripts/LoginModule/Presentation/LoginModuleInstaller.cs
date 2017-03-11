using Zenject;

public class LoginModuleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
//        Container.Bind<string>().FromInstance("Hello World!");
//        Container.Bind<Greeter>().AsSingle().NonLazy();
        Container.Bind<ILoginSystem>().To<LoginSystem>().AsTransient();
    }
}
