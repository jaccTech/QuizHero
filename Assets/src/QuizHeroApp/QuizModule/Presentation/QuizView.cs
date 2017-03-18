using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.xavi.QuizHero.QuizModule.Domain;
using UnityEngine.UI;
using com.xavi.QuizHero.QuizModule.Application;
using Zenject;

namespace com.xavi.QuizHero.QuizModule.Presentation
{
    public class QuizView : MonoBehaviour
    {
        [Inject] private IQuizApp _quizApp;
        [Inject] private QuizOptionView.Factory _quizOptionFactory;

        [SerializeField] private Text question;
        [SerializeField] private RectTransform optionsContainer;
        [SerializeField] private  Button submitButton;

        private bool loading;
        private Dictionary<int,QuizOptionView> options = new Dictionary<int, QuizOptionView>();
        private List<int> selectedOptions = new List<int>();

        public System.Action<bool> OnLoadingEvent;

        void Start()
        {
            submitButton.onClick.AddListener(HandleSubmitQuizButtonClick);
        }

        void OnEnable()
        {
            Debug.Log("QuizView.OnEnable");

            // clear view
            ClearView();

            FetchCurrentStage();
        }

        private void FetchCurrentStage()
        {
            if (!loading)
            {
                Debug.Log("QuizView.FetchCurrentStage");
                StartLoading();
                _quizApp.FetchCurrentStage(() =>
                    {
                        StartCoroutine(HandleUpdateStageEvent());
                    }
                );
            }
        }

        private IEnumerator HandleUpdateStageEvent()
        {
            Debug.Log("QuizView.HandleUpdateQuizView");
            // clear view
            ClearView();

            // question
            question.text = _quizApp.Stage.QuestionVO.Question;

            List<string> options = _quizApp.Stage.QuestionVO.Options;
            for (int i = 0; i < options.Count; i++)
            {
                AddOption(i, options[i]);

                yield return null;
            }

            StopLoading();
        }

        private void AddOption(int id, string text)
        {
            QuizOptionView optionView = _quizOptionFactory.Create();
            optionView.OptionSelectionChangedEvent += HandleOptionSelectedChanged;
            optionView.SetData(id, text);

            RectTransform optionTransform = optionView.gameObject.GetComponent<RectTransform>();
            optionTransform.SetParent(optionsContainer);
            optionTransform.localScale = Vector3.one;
            optionTransform.SetInsetAndSizeFromParentEdge(UnityEngine.RectTransform.Edge.Left, 0, optionsContainer.rect.size.x);

            options.Add(id, optionView);
        }

        private void HandleOptionSelectedChanged(QuizOptionView optionView)
        {
            if (optionView.IsSelected)
            {
                // deselect all if not multiselection
                if (!this._quizApp.Stage.QuestionVO.IsMultiselection)
                {
                    for (int i = 0; i < selectedOptions.Count; i++)
                    {
                        options[selectedOptions[i]].IsSelected = false;
                    }
                    selectedOptions.Clear();
                }

                // select last one
                selectedOptions.Add(optionView.Id);
            }
            else
            {
                selectedOptions.Remove(optionView.Id);
            }
        }

        private void HandleSubmitQuizButtonClick()
        {
            // validations
            if (selectedOptions.Count == 0)
                return;

            Debug.Log("HandleSubmitQuizButtonClick.selectedOptions: " + selectedOptions);
            
            this._quizApp.SubmitAnswer(
                new AnswerVO(selectedOptions),
                () => // onDone
                {
                    Debug.Log("HandleSubmitQuizButtonClick done");
                });
        }

        private void ClearView()
        {
            // question
            question.text = "";

            // children
            selectedOptions.Clear();
            options.Clear();
            QuizOptionView[] children = optionsContainer.GetComponentsInChildren<QuizOptionView>();
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    GameObject.Destroy(children[i].gameObject);
                }
            }
        }

        private void StartLoading()
        {
            loading = true;

            if (OnLoadingEvent != null)
                OnLoadingEvent(true);
        }

        private void StopLoading()
        {
            if (OnLoadingEvent != null)
                OnLoadingEvent(false);
            
            loading = false;
        }
    }
}