using com.xavi.QuizHero.QuizModule.Domain;
using UnityEngine;
using com.xavi.QuizHero.QuizModule.Presentation;
using Zenject;
using UnityEngine.UI;

namespace com.xavi.QuizHero.QuizModule.Application
{
    public class QuizApp : MonoBehaviour
    {
        [Inject] private IQuizSystem _quizSystem;

        [SerializeField] private QuestionView questionView;
        [SerializeField] private TimerView timeLeft;

        private QuestionVO currentQuestion;
//        private bool loading;

        void Start()
        {
            this._quizSystem.AddCurrentQuestionValueChangedListener(HandleCurrentquestionFetch);
            questionView.OnConfirmAnswerEvent += HandleConfirmAnswer;
        }

        void OnEnable()
        {
            questionView.EnableInput(false);
            questionView.ClearView();
//            FetchCurrentStage();
        }

        void OnDisable()
        {
//            this._quizSystem.UnregisterCurrentStageValueChangedListener(HandleCurrentStageFetch);
        }

        private void FetchCurrentStage()
        {
            this._quizSystem.FetchCurrentQuestion(HandleCurrentquestionFetch);
        }

        private void HandleCurrentquestionFetch(QuestionVO question)
        {
            Debug.Log("QuizApp.HandleCurrentquestionFetch " + question);
            currentQuestion = question;

            if (currentQuestion != null)
            {
                questionView.UpdateQuestion(
                    currentQuestion,
                    () => // onDoneCallback
                    {
                        // enable input
                        questionView.EnableInput(true);
                        // set timer
                        timeLeft.StartTimer(currentQuestion.time, HandleTimeOut);
                    }
                );
            }
            else
            {
                questionView.ClearView();
            }
        }

        private void HandleConfirmAnswer()
        {
            // stop timer
            timeLeft.StopTimer();

            // disable input
            questionView.EnableInput(false);

//            StartLoading();

            Debug.Log("HandleConfirmAnswer.selectedOptions: " + questionView.SelectedOptions);

            this._quizSystem.SubmitAnswer(
                new AnswerVO(currentQuestion.id, questionView.SelectedOptions, timeLeft.ElapsedTime),
                () => // onDone
                {
                    Debug.Log("HandleConfirmAnswer onDone");
//                    FetchCurrentQuiz();
                }
            );
        }

        private void HandleTimeOut()
        {
            Debug.Log("QuizApp.HandleTimeOut");

            // stop button and counter
            questionView.EnableInput(false);
            timeLeft.StopTimer();

            this._quizSystem.SubmitAnswer(
                new AnswerVO(currentQuestion.id, questionView.SelectedOptions, -1f),
                () => // onDone
                {
                    Debug.Log("HandleTimeOut onDone");
                }
            );
        }

//        private void StartLoading()
//        {
//            loading = true;
//        }
//
//        private void StopLoading()
//        {
//            loading = false;
//        }


        private void ClearView()
        {
            questionView.ClearView();
        }
    }
}
