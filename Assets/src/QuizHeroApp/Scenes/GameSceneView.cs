using UnityEngine;
using UnityEngine.UI;
using com.xavi.QuizHero.QuizModule.Presentation;

namespace com.xavi.QuizHero.GameModule.Presentation
{
    public class GameSceneView : MonoBehaviour
    {
        [SerializeField] private GameObject loadingPopup;

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