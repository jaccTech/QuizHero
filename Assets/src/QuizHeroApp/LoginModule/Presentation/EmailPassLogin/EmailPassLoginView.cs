using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using com.xavi.LoginModule.Domain;
using com.xavi.QuizHero.Domain.LoginSystem;

namespace com.xavi.QuizHero.LoginModule.Presentation.EmailPassLogin
{
    public class EmailPassLoginView : MonoBehaviour
    {
        [Inject] private ILoginSystem _loginController;

        public InputField email;
        public InputField pass;
        public FeedbackMessagePanel feedbackMessage;
        public Button registerButton;
        public Button loginButton;
        public Button cancelButton;

        // Use this for initialization
        void Start()
        {
            // hide message at the begining
            feedbackMessage.HideMessage();

            cancelButton.onClick.AddListener(HandleCancelButton);
            registerButton.onClick.AddListener(RequestEmailPassRegistration);
            loginButton.onClick.AddListener(RequestEmailPassLogin);
        }

        /// <summary>
        /// Requests the email pass login.
        /// </summary>
        private void RequestEmailPassLogin()
        {
            if (!ValidateEmail())
                return;

            _loginController.SignInWithEmailAndPasswordAsync(
                email.text,
                pass.text,
                HandleSignInRequestResult);
        }

        /// <summary>
        /// Requests the email pass registration.
        /// </summary>
        private void RequestEmailPassRegistration()
        {
            feedbackMessage.HideMessage();

            if (!ValidateEmail() || !ValidatePassword())
                return;

            _loginController.CreateUserWithEmailAndPasswordAsync(
                email.text,
                pass.text,
                HandleSignInRequestResult);
        }

        /// <summary>
        /// Handles the cancel button.
        /// </summary>
        private void HandleCancelButton ()
        {
            CloseView();
        }

        /// <summary>
        /// Handles the sign in request result.
        /// </summary>
        /// <param name="loginResult">Login result.</param>
        private void HandleSignInRequestResult(LoginResult loginResult)
        {
            switch (loginResult)
            {
                case LoginResult.ERROR:
                    feedbackMessage.ShowMessage("Invalid email and/or password...", FeedbackMessagePanel.StyleType.ERROR);
                    break;
                case LoginResult.CANCELLED:
                    break;
                case LoginResult.OK:
                    feedbackMessage.ShowMessage("SigIn OK!", FeedbackMessagePanel.StyleType.SUCCESS);
                    CloseView();
                    break;
            }
        }

        private void CloseView ()
        {
//            Debug.Log("CloseView");
            // destroy this game object so the scene is unload
            Destroy(gameObject);
            SceneManager.UnloadSceneAsync(SceneConstanst.SceneName.EmailPassLoginScene);
        }

        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <returns><c>true</c>, if email was validated, <c>false</c> otherwise.</returns>
        private bool ValidateEmail()
        {
            // validate email
            if (email == null || string.IsNullOrEmpty(email.text))
            {
                feedbackMessage.ShowMessage("Please enter an email.", FeedbackMessagePanel.StyleType.ERROR);
                return false;
            }
            else if (!TestEmail.IsEmail(email.text))
            {
                feedbackMessage.ShowMessage("Invalid email.", FeedbackMessagePanel.StyleType.ERROR);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <returns><c>true</c>, if password was validated, <c>false</c> otherwise.</returns>
        private bool ValidatePassword ()
        {
            if (pass == null || string.IsNullOrEmpty(pass.text))
            {
                feedbackMessage.ShowMessage("Please enter a password.", FeedbackMessagePanel.StyleType.ERROR);
                return false;
            }
            else if (!TestPassword.IsPassword(pass.text))
            {
                feedbackMessage.ShowMessage(TestPassword.ErrorMessage, FeedbackMessagePanel.StyleType.ERROR);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Helper class TestEmail
        /// Tests an E-Mail address.
        /// </summary>
        public static class TestEmail
        {
            /// <summary>
            /// Regular expression, which is used to validate an E-Mail address.
            /// </summary>
            public const string MatchEmailPattern =
                @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


            /// <summary>
            /// Checks whether the given Email-Parameter is a valid E-Mail address.
            /// </summary>
            /// <param name="email">Parameter-string that contains an E-Mail address.</param>
            /// <returns>True, wenn Parameter-string is not null and contains a valid E-Mail address;
            /// otherwise false.</returns>
            public static bool IsEmail(string email)
            {
                if (email != null)
                    return Regex.IsMatch(email, MatchEmailPattern);
                else
                    return false;
            }
        }

        /// <summary>
        /// Helper class TestPassword
        /// Test password.
        /// </summary>
        public static class TestPassword
        {
            public static Regex hasNumberRegex = new Regex(@"[0-9]+");
            public static Regex hasMiniMaxCharsRegex = new Regex(@".{8,15}");
            public static Regex hasUpperCharRegex = new Regex(@"[A-Z]+");
            public static Regex hasLowerCharRegex = new Regex(@"[a-z]+");
            public static Regex hasSymbolsRegex = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            /// <summary>
            /// Gets the error message.
            /// </summary>
            /// <value>The error message.</value>
            public static string ErrorMessage
            {
                get;
                private set;
            }

            /// <summary>
            /// Determines if is password is valid.
            /// </summary>
            /// <returns><c>true</c> if password is valid; otherwise, <c>false</c>.</returns>
            /// <param name="password">Password.</param>
            public static bool IsPassword(string password)
            {
                ErrorMessage = string.Empty;

                // check null or empty
                if (string.IsNullOrEmpty(password))
                    return false;

                // check length
                if (!hasMiniMaxCharsRegex.IsMatch(password))
                {
                    ErrorMessage = "Password should not be less than or greater than 8 characters";
                    return false;
                }
                // check numeric value
                else if (!hasNumberRegex.IsMatch(password))
                {
                    ErrorMessage = "Password should contain at least one numeric value";
                    return false;
                }
                // check lower char
                else if (!hasLowerCharRegex.IsMatch(password))
                {
                    ErrorMessage = "Password should contain At least one lower case letter";
                    return false;
                }
                // check upper char
                else if (!hasUpperCharRegex.IsMatch(password))
                {
                    ErrorMessage = "Password should contain At least one upper case letter";
                    return false;
                }
                // check symbol
                else if (!hasSymbolsRegex.IsMatch(password))
                {
                    ErrorMessage = "Password should contain At least one special case characters";
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}