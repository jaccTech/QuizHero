using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using com.xavi.QuizHero.DatabaseModule.Domain;

public class DatabaseModuleInstaller : MonoInstaller {
    public override void InstallBindings()
    {
        Container.Bind<IDatabaseSystem>().To<FirebaseDatabaseSystem>().AsSingle();
    }
}
