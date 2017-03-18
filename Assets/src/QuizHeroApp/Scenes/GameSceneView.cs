using UnityEngine;
using UnityEngine.UI;
using com.xavi.QuizHero.QuizModule.Presentation;

namespace com.xavi.QuizHero.GameModule.Presentation
{
    public class GameSceneView : MonoBehaviour
    {
        [SerializeField] private QuizView quizView;
        [SerializeField] private GameObject loadingPopup;

        void Awake ()
        {
            Debug.Log("GameSceneView.Awake");
            quizView.OnLoadingEvent += HandleLoadingPopupEvent;
        }

        private void HandleLoadingPopupEvent (bool active)
        {
            if (active)
            {
                ShowLoadingPopup();
            }
            else
            {
                HideLoadingPopup();
            }
        }
	
        private void ShowLoadingPopup ()
        {
            Debug.Log("GameSceneView.ShowLoadingPopup");
            loadingPopup.SetActive(true);
        }

        private void HideLoadingPopup ()
        {
            Debug.Log("GameSceneView.HideLoadingPopup");
            loadingPopup.SetActive(false);
        }
    }
}