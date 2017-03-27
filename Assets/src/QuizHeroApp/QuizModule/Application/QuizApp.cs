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

        private StageVO currentStage;
//        private bool loading;

        void Start()
        {
            questionView.OnConfirmAnswerEvent += HandleConfirmAnswer;
            this._quizSystem.RegisterCurrentStageValueChangedListener(HandleCurrentStageFetch);
        }

        void OnEnable()
        {
            questionView.EnableInput(false);
            questionView.ClearView();
            FetchCurrentStage();
        }

        private void FetchCurrentStage()
        {
            this._quizSystem.FetchCurrentStage(HandleCurrentStageFetch);
        }

        private void HandleCurrentStageFetch(StageVO stage)
        {
            Debug.Log("QuizApp.HandleCurrentStageFetch " + stage);
            currentStage = stage;

            if (currentStage != null)
            {
                questionView.UpdateQuestion(
                    currentStage.currentQuestion,
                    () => // onDoneCallback
                    {
                        // enable input
                        questionView.EnableInput(true);
                        // set timer
                        timeLeft.StartTimer(currentStage.currentQuestion.time, HandleTimeOut);
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
                currentStage.currentQuestion.id,
                new AnswerVO(questionView.SelectedOptions, timeLeft.ElapsedTime),
                () => // onDone
                {
                    Debug.Log("HandleSubmitQuizButtonClick done");
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
                currentStage.currentQuestion.id,
                new AnswerVO(questionView.SelectedOptions, -1f),
                () => // onDone
                {
                    Debug.Log("HandleTimeOut done");
                    //                    FetchCurrentQuiz();
                }
            );
        }


        public QuestionVO Question { get { return currentStage.currentQuestion; } }

        public void SubmitAnswer(AnswerVO answer, System.Action onDone)
        {
            this._quizSystem.SubmitAnswer(0, answer, onDone);
        }


        //        private void StartLoading()
        //        {
        //            loading = true;
        //
        //            if (OnLoadingEvent != null)
        //                OnLoadingEvent(true);
        //        }
        //
        //        private void StopLoading()
        //        {
        //            if (OnLoadingEvent != null)
        //                OnLoadingEvent(false);
        //
        //            loading = false;
        //        }


        private void ClearView()
        {
            questionView.ClearView();
        }
    }
}
