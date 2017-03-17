using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using com.xavi.QuizHero.QuizModule.Application;
using com.xavi.QuizHero.QuizModule.Domain;
using com.xavi.QuizHero.QuizModule.Presentation;

public class QuizModuleInstaller : MonoInstaller
{
    public GameObject quizOptionPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IQuizSystem>().To<QuizSystem>().AsSingle();
        Container.Bind<IQuizApp>().To<QuizApp>().AsSingle();
        Container.BindFactory<QuizOptionView, QuizOptionView.Factory>().FromComponentInNewPrefab(quizOptionPrefab);
    }
}
