using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.SceneManagement;
using com.xavi.LoadingSpinnerModule;
using com.xavi.LoginModule.Domain;
using com.xavi.QuizHero.Domain.LoginSystem;

namespace com.xavi.QuizHero.LoginModule.Application
{
    public class LoginApp : ILoginApp
    {
        [Inject] private ILoginSystem _loginController;
        [Inject] private ILoadingSpinnerSystem _loadingSpinnerSystem;

        public Canvas canvas;

        // Delegates to notify the UI
        private event LoginStateChangedEvent loginStateChangedEvent;

        public void RegisterLoginStateChangedListener(LoginStateChangedEvent listener)
        {
            loginStateChangedEvent += listener;
        }

        public void UnregisterLoginStateChangedListener(LoginStateChangedEvent listener)
        {
            loginStateChangedEvent -= listener;
        }

        public void Initialize(System.Action onDone = null)
        {
            if (_loginController != null)
            {
                _loginController.Initialize(() =>
                    {
                        _loginController.RegisterAuthStateChangedListener(HandleAuthStateChangedEvent);

                        if (onDone != null)
                            onDone();
                    });
            }
        }

        public bool IsSignedIn { get { return _loginController.IsSignedIn(); } }

        public void StartEmailPassSignIn()
        {
            // Open EmailPassLogin Component
            SceneManager.LoadScene(SceneConstanst.SceneName.EmailPassLoginScene, LoadSceneMode.Additive);
        }

        public void StartGoogleSignIn()
        {
            
        }

        public void StartFacebookSignIn()
        {
            
        }

        private void HandleAuthStateChangedEvent()
        {
            Debug.Log("HandleAuthStateChangedEvent");
            if (loginStateChangedEvent != null)
                loginStateChangedEvent();
        }
    }
}