using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using com.xavi.QuizHero.QuizModule.Domain;

namespace com.xavi.QuizHero.QuizModule.Presentation
{
    public class HomeQuizOptionView : MonoBehaviour
    {
        [SerializeField] private Text description;
        [SerializeField] Image selectedStateBackground;
        [SerializeField] Image unselectedStateBackground;

        private bool isSelected;

        public QuizVO Data { get; private set; }

        public delegate void OptionSelectionChangedEventDelegate(HomeQuizOptionView quiz);

        public event OptionSelectionChangedEventDelegate OptionSelectionChangedEvent;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                UpdateVisuals();
            }
        }

        public void SetData(QuizVO quiz)
        {
            this.Data = quiz;
            description.text = quiz.description;
            IsSelected = false;
        }

        public void HandleClick()
        {
            IsSelected = !IsSelected;

            if (OptionSelectionChangedEvent != null)
                OptionSelectionChangedEvent(this);
        }

        void UpdateVisuals()
        {
            selectedStateBackground.enabled = IsSelected;
            unselectedStateBackground.enabled = !IsSelected;
        }

        public class Factory : Factory<HomeQuizOptionView>
        {
        }
    }
}