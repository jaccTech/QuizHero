using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.xavi.LoadingSpinnerModule
{
    public class LoadingSpinnerSystem : ILoadingSpinnerSystem
    {
        private bool spinnerActive = false;
        private Dictionary<SpinnerType,string> spinners = new Dictionary<SpinnerType, string>()
        {
            { SpinnerType.SIMPLE, "SimpleLoadingSpinner" }
        };

        private string currentSpinnerSceneName;

        public void StartSpinner(SpinnerType spinnerType)
        {
            if (spinnerActive)
            {
                Debug.LogWarning("Spinner already active.");
                return;
            }

            SceneManager.LoadSceneAsync(spinners[spinnerType], LoadSceneMode.Additive);
            currentSpinnerSceneName = spinners[spinnerType];

            Debug.Log("currentSpinnerScene " + currentSpinnerSceneName);
        }

        public void StopSpinner()
        {
            Debug.Log("StopSpinner... " + currentSpinnerSceneName);
            if (string.IsNullOrEmpty(currentSpinnerSceneName))
            {
                Debug.LogWarning("No Spinner Scene active.");
                return;
            }

            SceneManager.UnloadSceneAsync(currentSpinnerSceneName);

        }
    }
}