
namespace com.xavi.QuizHero.LoginModule.Application
{
    public delegate void LoginStateChangedEvent();

    public interface ILoginApp
    {
        void Initialize(System.Action onDone = null);

        bool IsSignedIn { get; }

        void StartEmailPassSignIn();

        void StartGoogleSignIn();

        void StartFacebookSignIn();

        void RegisterLoginStateChangedListener(LoginStateChangedEvent listener);

        void UnregisterLoginStateChangedListener(LoginStateChangedEvent listener);
    }
}