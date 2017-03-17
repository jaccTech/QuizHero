using Zenject;
using com.xavi.LoadingSpinnerModule;
using com.xavi.LoginModule.Domain;
using com.xavi.QuizHero.Domain.LoginSystem;
using com.xavi.QuizHero.LoginModule.Application;

namespace com.xavi.QuizHero.LoginModule.Application.Installer
{
    public class LoginModuleInstaller : MonoInstaller<LoginModuleInstaller>
    {
        public override void InstallBindings()
        {
//        Container.Bind<string>().FromInstance("Hello World!");
//        Container.Bind<Greeter>().AsSingle().NonLazy();

            Container.Bind<ILoadingSpinnerSystem>().To<LoadingSpinnerSystem>().AsSingle();
            Container.Bind<ILoginSystem>().To<FirebaseLoginSystem>().AsSingle();
            Container.Bind<ILoginApp>().To<LoginApp>().AsSingle();
        }
    }
}