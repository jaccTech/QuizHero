namespace com.xavi.LoginModule.Domain
{
    public enum LoginResult
    {
        NONE,
        OK,
        CANCELLED,
        ERROR
    }

    public interface ILoginSystem
    {
    //    void StartLogin(ILoginData loginData, System.Action<ILoginUser> successCallback, System.Action<LoginError> errorCallback);
        void Initialize(System.Action onDone);
        bool IsSignedIn();
        void CreateUserWithEmailAndPasswordAsync(string email, string password, System.Action<LoginResult> onDone);
        void SignInWithEmailAndPasswordAsync();
    }
}