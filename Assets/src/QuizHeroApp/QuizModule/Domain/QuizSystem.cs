using com.xavi.QuizHero.DatabaseModule.Domain;
using UnityEngine;
using com.xavi.LoginModule.Domain;
using System;
using System.Collections.Generic;


namespace com.xavi.QuizHero.QuizModule.Domain
{
    public class QuizSystem : IQuizSystem
    {
        private IDatabaseSystem _databaseSystem;
        private ILoginSystem _loginSystem;

        public QuizSystem(IDatabaseSystem databaseSystem, ILoginSystem loginSystem)
        {
            this._databaseSystem = databaseSystem;
            this._loginSystem = loginSystem;
        }

        public void FetchCurrentStage(QuizUpdatedEvent quizUpdatedEventListener)
        {
            string userId = _loginSystem.LoginData.UserId;
//            Debug.Log("FetchCurrentQuiz userId: " + userId);

            // get stage
            this._databaseSystem.GetValueAsync<StageVO>("users/" + userId + "/stage", (StageVO stage) =>
                {
//                    Debug.Log("FetchCurrentQuiz stage: " + stage);

                    // get quiz
//                    Debug.Log("Calling path... " + "quiz/" + stage.level + "/" + stage.quiz);
                    this._databaseSystem.GetValueAsync<QuizVO>("quiz/" + stage.level + "/" + stage.quiz, (QuizVO quiz) =>
                        {
//                            Debug.Log("FetchCurrentQuiz quiz: " + quiz);
                            stage.quizVO = quiz;
                            quizUpdatedEventListener(stage);
                        });
                });
        }
    }
}