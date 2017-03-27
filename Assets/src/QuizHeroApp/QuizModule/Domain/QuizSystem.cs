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

        public void FetchAvailableQuizzes(System.Action<List<QuizVO>> onDoneCallback)
        {
            // get quiz list
            this._databaseSystem.GetListValueAsync<QuizVO>(
                "/quizzes",
                (List<QuizVO> quizzes) =>
                {
                    onDoneCallback(quizzes);
                }
            );
        }

        public void UpdateSelectedQuiz(QuizVO quiz, System.Action onDoneCallback)
        {
            Debug.Log("UpdateSelectedQuiz quiz " + quiz);
            this._databaseSystem.SetRawJsonValueAsync(
                "users/" + UserId + "/selectedQuiz",
                JsonUtility.ToJson(quiz),
                onDoneCallback);
        }

        public void FetchCurrentStage(System.Action<StageVO> onDoneCallback)
        {
            this._databaseSystem.GetValueAsync<StageVO>(
                "users/" + UserId + "/currentStage",
                (StageVO stage) =>
                {
                    onDoneCallback(stage);
                }
            );
        }

        public void RegisterCurrentStageValueChangedListener(System.Action<StageVO> onDoneCallback)
        {
            this._databaseSystem.RegisterValueChangedListener<StageVO>(
                "users/" + UserId + "/currentStage",
                onDoneCallback
            );
        }

        public void SubmitAnswer(long quizId, AnswerVO answer, System.Action onDoneCallback)
        {
            Debug.Log("submiting answer " + answer);
            this._databaseSystem.SetRawJsonValueAsync(
                "answers/" + UserId + "/" + quizId,
                JsonUtility.ToJson(answer),
                onDoneCallback);
        }
    }
}