using com.xavi.QuizHero.DatabaseModule.Domain;
using UnityEngine;
using com.xavi.LoginModule.Domain;
using System;
using System.Collections.Generic;


namespace com.xavi.QuizHero.QuizModule.Domain
{
    static class ObjectReference
    {
        public static string QUIZ_LIST_REF = "/quizzes";
        public static string SELECTED_QUIZ_REF(string uid) { return "users/" + uid + "/currentStage/selectedQuiz"; }
        public static string CURRENT_ANSWER_REF(string uid) { return "users/" + uid + "/currentStage/currentAnswer"; }
        public static string CURRENT_QUESTION_REF(string uid) { return "users/" + uid + "/currentStage/currentQuestion"; }
    }

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
                ObjectReference.QUIZ_LIST_REF,
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
                ObjectReference.SELECTED_QUIZ_REF(UserId),
                JsonUtility.ToJson(quiz),
                onDoneCallback);
        }

        public void FetchCurrentQuestion(System.Action<QuestionVO> onDoneCallback)
        {
            this._databaseSystem.GetValueAsync<QuestionVO>(
                ObjectReference.CURRENT_QUESTION_REF(UserId),
                onDoneCallback
            );
        }

        public void AddCurrentQuestionValueChangedListener(System.Action<QuestionVO> onDoneCallback)
        {
            this._databaseSystem.RegisterValueChangedListener<QuestionVO>(
                ObjectReference.CURRENT_QUESTION_REF(UserId),
                onDoneCallback
            );
        }

        public void SubmitAnswer(AnswerVO answer, System.Action onDoneCallback)
        {
            Debug.Log("submiting answer " + answer);
            this._databaseSystem.SetRawJsonValueAsync(
                ObjectReference.CURRENT_ANSWER_REF(UserId),
                JsonUtility.ToJson(answer),
                onDoneCallback);
        }
    }
}