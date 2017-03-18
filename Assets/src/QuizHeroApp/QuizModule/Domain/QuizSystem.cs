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

        private string UserId { get { return _loginSystem.LoginData.UserId; } }

        public QuizSystem(IDatabaseSystem databaseSystem, ILoginSystem loginSystem)
        {
            this._databaseSystem = databaseSystem;
            this._loginSystem = loginSystem;
        }

        public void FetchCurrentStage(QuizUpdatedEvent quizUpdatedEventListener)
        {
            // get stage
            this._databaseSystem.GetValueAsync<StageVO>("users/" + UserId + "/stage", (StageVO stage) =>
                {
                    // get quiz
                    this._databaseSystem.GetValueAsync<QuestionVO>("questions/" + stage.question, (QuestionVO quiz) =>
                        {
                            stage.questionVO = quiz;
                            quizUpdatedEventListener(stage);
                        });
                });
        }

        public void SubmitAnswer(long question, IAnswerVO answer, System.Action onDoneCallback)
        {
            Debug.Log("submiting answer " + answer);
            this._databaseSystem.SetRawJsonValueAsync(
                "answers/" + UserId + "/" + question,
                JsonUtility.ToJson(answer),
                onDoneCallback);
        }
    }
}