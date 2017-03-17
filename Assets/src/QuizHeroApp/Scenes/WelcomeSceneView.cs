using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.xavi.QuizHero.Domain.LoginSystem;
using Zenject;
using com.xavi.QuizHero.LoginModule.Application;

namespace com.xavi.QuizHero.Scenes
{
    public class WelcomeSceneView : MonoBehaviour
    {
        [Inject] private ILoginApp _loginApp;

        public Canvas canvas;

        // Use this for initialization
        void Start()
        {
            // hide options
            canvas.enabled = false;

            if (_loginApp != null)
            {
                _loginApp.Initialize(() => {
                    if (_loginApp.IsSignedIn)
                    {
                        GoHome(null);
                    }
                    else
                    {
                        // keep listening...
                        _loginApp.RegisterLoginStateChangedListener(LoginChangedListener);

                        // show options
                        canvas.enabled = true;
                    }
                });
            }
        }

        public void LoginChangedListener()
        {
            Debug.Log("LoginChangedListener");
            if (_loginApp.IsSignedIn)
            {
                // go home if already logged in
                GoHome(null);
            }
        }

        public void HandleEmailPassSignInSelection()
        {
            _loginApp.StartEmailPassSignIn();
        }

        public void HandleGoogleSignInSelection()
        {
            _loginApp.StartGoogleSignIn();
        }

        public void HandleFacebookSignInSelection()
        {
            _loginApp.StartFacebookSignIn();
        }

        private void GoHome(ILoginUser loginUser)
        {
            SceneManager.LoadScene(SceneConstanst.SceneName.HomeScene, LoadSceneMode.Single);
        }
    }
}
