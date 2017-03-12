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

        /// <summary>
        /// Determines whether this instance is signed in.
        /// </summary>
        /// <returns><c>true</c> if this instance is signed in; otherwise, <c>false</c>.</returns>
        bool IsSignedIn();

        /// <summary>
        /// Creates the user with email and password async.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="onDone">On done.</param>
        void CreateUserWithEmailAndPasswordAsync(string email, string password, System.Action<LoginResult> onDone);


        void SignInWithEmailAndPasswordAsync();

        /// <summary>
        /// Registers the auth state changed listener.
        /// </summary>
        /// <param name="listener">Listener.</param>
        void RegisterAuthStateChangedListener(System.Action listener);

        /// <summary>
        /// Unregisters the auth state changed listener.
        /// </summary>
        /// <param name="listener">Listener.</param>
        void UnregisterAuthStateChangedListener(System.Action listener);
    }
}