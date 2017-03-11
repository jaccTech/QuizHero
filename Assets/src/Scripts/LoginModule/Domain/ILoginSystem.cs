public interface ILoginSystem
{
    void StartLogin(ILoginData loginData, System.Action<ILoginUser> successCallback, System.Action<LoginError> errorCallback);
}
