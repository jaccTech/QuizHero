using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using com.xavi.QuizHero.QuizModule.Application;
using com.xavi.QuizHero.QuizModule.Domain;

public class QuizModuleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IQuizSystem>().To<QuizSystem>().AsSingle();
        Container.Bind<IQuizApp>().To<QuizApp>().AsSingle();
    }
}
