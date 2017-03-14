using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.SceneManagement;
using com.xavi.LoadingSpinnerModule;
using com.xavi.LoginModule.Domain;
using com.xavi.QuizHero.Domain.LoginSystem;

public class LoginView : MonoBehaviour
{
    [Inject] private ILoginSystem _loginController;
    [Inject] private ILoadingSpinnerSystem _loadingSpinnerSystem;

    public Canvas canvas;

    void Start()
    {
        // hide options
        canvas.enabled = false;

        if (_loginController != null)
        {
//            _loadingSpinnerSystem.StartSpinner(SpinnerType.SIMPLE);
            _loginController.Initialize(HandleLoginControllerInitialized);
        }
    }

    private void HandleLoginControllerInitialized()
    {
//        _loadingSpinnerSystem.StopSpinner();

        if (_loginController.IsSignedIn())
        {
            // go home if already logged in
            GoHome(null);
        }
        else
        {
            _loginController.RegisterAuthStateChangedListener(HandleAuthStateChangedEvent);

            // enable login options
            canvas.enabled = true;
        }
    }

    public void HandleAuthStateChangedEvent ()
    {
        if (_loginController.IsSignedIn())
        {
            // go home if already logged in
            GoHome(null);
        }
    }

    public void HandleEmailPassSignInSelection()
    {
        SceneManager.LoadScene(SceneConstanst.SceneName.EmailPassLoginScene, LoadSceneMode.Additive);
    }

    private void GoHome(ILoginUser loginUser)
    {
        SceneManager.LoadScene(SceneConstanst.SceneName.HomeScene, LoadSceneMode.Single);
    }
}
