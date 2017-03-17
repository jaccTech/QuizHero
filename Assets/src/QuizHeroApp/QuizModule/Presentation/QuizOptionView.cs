using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.xavi.QuizHero.QuizModule.Presentation
{
    public class QuizOptionView : MonoBehaviour
    {
        [SerializeField] private Text description;
        [SerializeField] Image selectedStateBackground;
        [SerializeField] Image unselectedStateBackground;

        public int Id { get; private set; }
        public bool IsSelected { get; private set; }

        public delegate void OptionSelectionChangedEventDelegate(QuizOptionView quiz);
        public event OptionSelectionChangedEventDelegate OptionSelectionChangedEvent;

        public void SetData (int id, string text)
        {
            this.Id = id;
            description.text = text;
            IsSelected = false;
            UpdateVisuals();
        }

        public void HandleClick ()
        {
            IsSelected = !IsSelected;
            UpdateVisuals();

            if (OptionSelectionChangedEvent != null)
                OptionSelectionChangedEvent(this);
        }

        void UpdateVisuals ()
        {
            selectedStateBackground.enabled = IsSelected;
            unselectedStateBackground.enabled = !IsSelected;
        }
    }
}