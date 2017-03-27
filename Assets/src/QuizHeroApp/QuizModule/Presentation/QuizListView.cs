using UnityEngine;
using System.Collections;
using com.xavi.QuizHero.QuizModule.Domain;
using System.Collections.Generic;
using Zenject;

namespace com.xavi.QuizHero.QuizModule.Presentation
{
    public class QuizListView : MonoBehaviour
    {
        [Inject] private HomeQuizOptionView.Factory _homeQuizOptionFactory;

        [SerializeField] private RectTransform container;

        public System.Action<QuizVO> OnQuizSelectedEvent;

        void OnEnable()
        {
            // clear view
            ClearView();
        }

        public void UpdateList (List<QuizVO> quizzes)
        {
            Debug.Log("QuizListView.UpdateList: " + quizzes.Count);

            StartCoroutine(UpdateListCoroutine(quizzes));
        }

        private IEnumerator UpdateListCoroutine(List<QuizVO> quizzes)
        {
            // clear view
            ClearView();

            for (int i = 0; i < quizzes.Count; i++)
            {
                AddQuizItem(quizzes[i]);

                yield return null;
            }
        }

        private void AddQuizItem (QuizVO quiz)
        {
            HomeQuizOptionView optionView = _homeQuizOptionFactory.Create();
            optionView.OptionSelectionChangedEvent += HandleOptionSelected;
            optionView.SetData(quiz);

            RectTransform optionTransform = optionView.gameObject.GetComponent<RectTransform>();
            optionTransform.SetParent(container);
            optionTransform.localScale = Vector3.one;
            optionTransform.SetInsetAndSizeFromParentEdge(UnityEngine.RectTransform.Edge.Left, 0, container.rect.size.x);
        }

        private void ClearView()
        {
            HomeQuizOptionView[] children = container.GetComponentsInChildren<HomeQuizOptionView>();
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    GameObject.Destroy(children[i].gameObject);
                }
            }
        }

        private void HandleOptionSelected(HomeQuizOptionView optionView)
        {
            if (optionView.IsSelected)
            {
                if (OnQuizSelectedEvent != null)
                {
                    OnQuizSelectedEvent(optionView.Data);
                }
            }
        }
    }
}