using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.SceneManagement;
using com.xavi.LoadingSpinnerModule;
using com.xavi.QuizGame.SceneModule.Domain;
using com.xavi.LoginModule.Domain;

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
            // enable login options
            canvas.enabled = true;
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
