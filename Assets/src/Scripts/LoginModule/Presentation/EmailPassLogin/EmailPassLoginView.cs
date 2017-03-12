using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using com.xavi.QuizGame.SceneModule.Domain;
using com.xavi.LoginModule.Domain;

namespace com.xavi.LoginModule.Presentation.EmailPassLogin
{
    public class EmailPassLoginView : MonoBehaviour
    {
        [Inject] private ILoginSystem _loginController;


        public InputField email;
        public Button goButton;
        public FeedbackMessagePanel feedbackMessage;

        // Use this for initialization
        void Start()
        {
            feedbackMessage.HideMessage();
        }

        /// <summary>
        /// Requests the email pass login.
        /// </summary>
        public void RequestEmailPassLogin()
        {
            if (!ValidateInput())
                return;


        }

        /// <summary>
        /// Requests the email pass registration.
        /// </summary>
        public void RequestEmailPassRegistration()
        {
            feedbackMessage.HideMessage();

            if (!ValidateInput())
                return;

            _loginController.CreateUserWithEmailAndPasswordAsync(
                email.text,
                "somepass",
                HandleRegistrationRequestResult);
        }

        private void HandleRegistrationRequestResult(LoginResult loginResult)
        {
            switch (loginResult)
            {
                case LoginResult.ERROR:
                    feedbackMessage.ShowMessage("Something went wrong...", FeedbackMessagePanel.StyleType.ERROR);
                    break;
                case LoginResult.CANCELLED:
                    break;
                case LoginResult.OK:
                    feedbackMessage.ShowMessage("Registration OK.", FeedbackMessagePanel.StyleType.SUCCESS);
                    CloseView();
                    break;
            }
        }

        /// <summary>
        /// Handles the cancel button.
        /// </summary>
        public void HandleCancelButton ()
        {
//            SceneManager.LoadScene(SceneConstanst.SceneName.WelcomeScene, LoadSceneMode.Single);
            CloseView();
        }

        private void CloseView ()
        {
//            Debug.Log("CloseView");
            // destroy this game object so the scene is unload
            Destroy(gameObject);
            SceneManager.UnloadSceneAsync(SceneConstanst.SceneName.EmailPassLoginScene);
        }

        /// <summary>
        /// Validates the input.
        /// </summary>
        /// <returns><c>true</c>, if input was validated, <c>false</c> otherwise.</returns>
        private bool ValidateInput()
        {
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

            feedbackMessage.ShowMessage("Email OK.", FeedbackMessagePanel.StyleType.SUCCESS);
            return true;
        }

        /// <summary>
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
    }
}