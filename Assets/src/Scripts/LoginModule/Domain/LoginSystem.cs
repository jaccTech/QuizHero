using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSystem : ILoginSystem
{
    public void StartLogin(ILoginData loginData, System.Action<ILoginUser> successCallback, System.Action<LoginError> errorCallback)
    {
        successCallback(null);
    }
}
