using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.xavi.QuizHero.QuizModule.Domain;
using Zenject;
using com.xavi.QuizHero.QuizModule.Presentation;
using UnityEngine.SceneManagement;
using com.xavi.QuizHero.Domain.LoginSystem;

namespace com.xavi.QuizHero.QuizModule.Application
{
    public class HomeApp : MonoBehaviour
    {
        [Inject] private IQuizSystem _quizSystem;

        [SerializeField] private QuizListView quizListView;

        void Start ()
        {
            FetchAvailableQuizzes();

            quizListView.OnQuizSelectedEvent += HandleSelectedQuiz;
        }

        private void FetchAvailableQuizzes ()
        {
            _quizSystem.FetchAvailableQuizzes(HandleFetchAvailableQuizzes);
        }

        private void HandleFetchAvailableQuizzes (List<QuizVO> quizzes)
        {
            quizListView.UpdateList(quizzes);
        }

        private void HandleSelectedQuiz(QuizVO quiz)
        {
            Debug.Log("HandleSelectedQuiz");
            _quizSystem.UpdateSelectedQuiz(quiz, () => {
                Debug.Log("HandleSelectedQuiz Finished.");
                SceneManager.LoadSceneAsync(SceneConstanst.SceneName.GameScene, LoadSceneMode.Single);
            });
        }
    }
}