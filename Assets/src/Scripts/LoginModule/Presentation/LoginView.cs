using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.SceneManagement;

public class LoginView : MonoBehaviour
{
    [Inject]
    private ILoginSystem _loginController;

    void Start()
    {
        if (_loginController != null)
        {
            _loginController.StartLogin(null, OnLoginSuccess, null);
        }
    }

    private void OnLoginSuccess(ILoginUser loginUser)
    {
        SceneManager.LoadScene("HomeScene", LoadSceneMode.Single);
    }
}
