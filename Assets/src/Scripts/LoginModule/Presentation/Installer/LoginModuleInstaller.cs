using Zenject;
using com.xavi.LoadingSpinnerModule;
using com.xavi.LoginModule.Domain;
using com.xavi.QuizHero.Domain.LoginSystem;

namespace com.xavi.LoginModule.Presentation.Installer
{
    public class LoginModuleInstaller : MonoInstaller<LoginModuleInstaller>
    {
        public override void InstallBindings()
        {
//        Container.Bind<string>().FromInstance("Hello World!");
//        Container.Bind<Greeter>().AsSingle().NonLazy();

            Container.Bind<ILoadingSpinnerSystem>().To<LoadingSpinnerSystem>().AsSingle();
            Container.Bind<ILoginSystem>().To<FirebaseLoginSystem>().AsSingle();
        }
    }
}