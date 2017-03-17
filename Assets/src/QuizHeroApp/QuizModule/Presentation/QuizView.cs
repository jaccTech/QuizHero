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

        [SerializeField] private Text question;
        [SerializeField] private GameObject optionPrefab;
        [SerializeField] private RectTransform optionsContainer;

        private bool loading;
        private List<int> selectedOption = new List<int>();

        public System.Action<bool> OnLoadingEvent;

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
            question.text = _quizApp.Stage.QuizVO.Question;

            yield return null;

            List<string> options = _quizApp.Stage.QuizVO.Options;
            for (int i = 0; i < options.Count; i++)
            {
                RectTransform optionTransform = Instantiate<Transform>(optionPrefab.transform).GetComponent<RectTransform>();
                optionTransform.SetParent(optionsContainer);
                optionTransform.localScale = Vector3.one;
                optionTransform.SetInsetAndSizeFromParentEdge (UnityEngine.RectTransform.Edge.Left, 0, optionsContainer.rect.size.x);

                QuizOptionView optionView = optionTransform.GetComponent<QuizOptionView>();
                optionView.OptionSelectionChangedEvent += HandleOptionSelectedChanged;
                optionView.SetData(i,options[i]);

                yield return null;
            }

            StopLoading();
        }

        private void HandleOptionSelectedChanged (QuizOptionView optionView)
        {
            if (optionView.IsSelected)
            {
                Debug.Log("Selected... " + optionView.Id);
                selectedOption.Add(optionView.Id);
            }
            else
            {
                Debug.Log("Deselected... " + optionView.Id);
                selectedOption.Remove(optionView.Id);
            }

        }

        private void ClearView()
        {
            // question
            question.text = "";

            // children
            selectedOption.Clear();
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