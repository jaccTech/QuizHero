using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.xavi.QuizHero.QuizModule.Domain;
using UnityEngine.UI;
using com.xavi.QuizHero.QuizModule.Application;
using Zenject;

namespace com.xavi.QuizHero.QuizModule.Presentation
{
    public class QuestionView : MonoBehaviour
    {
        [Inject] private AswerOptionView.Factory _quizOptionFactory;

        [SerializeField] private Text questionText;
        [SerializeField] private RectTransform optionsContainer;
        [SerializeField] private Button confirmtButton;

        private Dictionary<int,AswerOptionView> options = new Dictionary<int, AswerOptionView>();
        private List<int> selectedOptions = new List<int>();
        private QuestionVO currentQuestion;

        public System.Action OnConfirmAnswerEvent;

        public List<int> SelectedOptions { get { return selectedOptions; } }

        void Start()
        {
            confirmtButton.onClick.AddListener(HandleConfirmButtonClick);
            confirmtButton.enabled = false;
        }

        public void UpdateQuestion(QuestionVO question, System.Action onDoneCallback)
        {
            currentQuestion = question;

            StartCoroutine(UpdateQuestionCoroutine(onDoneCallback));
        }

        private IEnumerator UpdateQuestionCoroutine(System.Action onDoneCallback)
        {
            Debug.Log("QuestionView.UpdateQuestionCoroutine");
            // clear view
            ClearView();

            // question
            questionText.text = currentQuestion.question;

            List<string> options = currentQuestion.options;
            for (int i = 0; i < options.Count; i++)
            {
                AddOption(i, options[i]);

                yield return null;
            }

            if (onDoneCallback != null)
                onDoneCallback();
        }

        private void AddOption(int id, string text)
        {
            AswerOptionView optionView = _quizOptionFactory.Create();
            optionView.OptionSelectionChangedEvent += HandleOptionSelectedChanged;
            optionView.SetData(id, text);

            RectTransform optionTransform = optionView.gameObject.GetComponent<RectTransform>();
            optionTransform.SetParent(optionsContainer);
            optionTransform.localScale = Vector3.one;
            optionTransform.SetInsetAndSizeFromParentEdge(UnityEngine.RectTransform.Edge.Left, 0, optionsContainer.rect.size.x);

            options.Add(id, optionView);
        }

        private void HandleOptionSelectedChanged(AswerOptionView optionView)
        {
            if (optionView.IsSelected)
            {
                // deselect all if not multiselection
                if (!this.currentQuestion.isMultiselection)
                {
                    for (int i = 0; i < selectedOptions.Count; i++)
                    {
                        options[selectedOptions[i]].IsSelected = false;
                    }
                    selectedOptions.Clear();
                }

                // select current one
                selectedOptions.Add(optionView.Id);
            }
            else
            {
                // deselect current one
                selectedOptions.Remove(optionView.Id);
            }

            // enable button when something is selected
            confirmtButton.enabled = selectedOptions.Count > 0;
        }

        private void HandleConfirmButtonClick()
        {
            // validations
            if (currentQuestion.options.Count == 0)
                return;
            
            Debug.Log("HandleConfirmButtonClick.selectedOptions: " + currentQuestion.options);

            if (OnConfirmAnswerEvent != null)
                OnConfirmAnswerEvent();
        }

        public void EnableInput (bool enable)
        {
            confirmtButton.enabled = enable;
        }

        public void ClearView()
        {
            // question
            questionText.text = "";

            // children
            selectedOptions.Clear();
            options.Clear();
            AswerOptionView[] children = optionsContainer.GetComponentsInChildren<AswerOptionView>();
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    GameObject.Destroy(children[i].gameObject);
                }
            }
        }
    }
}