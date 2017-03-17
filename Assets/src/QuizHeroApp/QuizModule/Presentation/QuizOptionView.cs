using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace com.xavi.QuizHero.QuizModule.Presentation
{
    public class QuizOptionView : MonoBehaviour
    {
        [SerializeField] private Text description;
        [SerializeField] Image selectedStateBackground;
        [SerializeField] Image unselectedStateBackground;

        private bool isSelected;

        public int Id { get; private set; }

        public delegate void OptionSelectionChangedEventDelegate(QuizOptionView quiz);

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

        public void SetData(int id, string text)
        {
            this.Id = id;
            description.text = text;
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

        public class Factory : Factory<QuizOptionView>
        {
        }
    }
}