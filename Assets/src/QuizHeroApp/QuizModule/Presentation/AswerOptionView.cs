using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace com.xavi.QuizHero.QuizModule.Presentation
{
    public class AswerOptionView : MonoBehaviour
    {
        [SerializeField] private Text description;
        [SerializeField] Image selectedStateBackground;
        [SerializeField] Image unselectedStateBackground;

        private bool isSelected;

        public int Id { get; private set; }

        public delegate void OptionClickedEventDelegate(AswerOptionView quiz);

        public event OptionClickedEventDelegate OptionClickedEvent;

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
            if (OptionClickedEvent != null)
                OptionClickedEvent(this);
        }

        void UpdateVisuals()
        {
            selectedStateBackground.enabled = IsSelected;
            unselectedStateBackground.enabled = !IsSelected;
        }

        public class Factory : Factory<AswerOptionView>
        {
        }
    }
}