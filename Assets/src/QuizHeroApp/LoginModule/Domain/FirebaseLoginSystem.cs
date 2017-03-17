using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System.Threading.Tasks;
using com.xavi.LoginModule.Domain;

namespace com.xavi.QuizHero.Domain.LoginSystem
{
    /// <summary>
    /// Firebase login system implements ILoginSystem to handle Login process.
    /// </summary>
    public class FirebaseLoginSystem : ILoginSystem
    {
        private FirebaseAuth auth;
        private FirebaseUser user;
        private QuizHeroLoginData loginData;
        private bool initialized;

        private Firebase.DependencyStatus dependencyStatus;

        /// <summary>
        /// Initialize the FirebaseLoginSystem
        /// </summary>
        /// <param name="onDone">Callback when done</param>
        public void Initialize(System.Action onDone = null)
        {
            if (initialized)
                return;
        
            CheckDependencies(() =>
                {
                    InitializeFirebase();
                    if (onDone != null)
                        onDone();
                });
        }

        // When the app starts, check to make sure that we have
        // the required dependencies to use Firebase, and if not,
        // add them if possible.
        void CheckDependencies(System.Action onDone)
        {
            dependencyStatus = Firebase.FirebaseApp.CheckDependencies();
            if (dependencyStatus != Firebase.DependencyStatus.Available)
            {
                Firebase.FirebaseApp.FixDependenciesAsync().ContinueWith(task =>
                    {
                        dependencyStatus = Firebase.FirebaseApp.CheckDependencies();
                        if (dependencyStatus == Firebase.DependencyStatus.Available)
                        {
                            onDone();
                        }
                        else
                        {
                            Debug.LogError(
                                "Could not resolve all Firebase dependencies: " + dependencyStatus);
                        }
                    });
            }
            else
            {
                onDone();
            }
        }

        // Handle initialization of the necessary firebase modules:
        void InitializeFirebase()
        {
            if (initialized)
                return;
        
            Debug.Log("Setting up Firebase Auth");
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);

            initialized = true;
        }

        /// <summary>
        /// Determines whether this instance is signed in.
        /// </summary>
        /// <returns><c>true</c> if this instance is signed in; otherwise, <c>false</c>.</returns>
        public bool IsSignedIn()
        {
            return user != auth.CurrentUser && auth.CurrentUser != null;
        }

        /// <summary>
        /// Creates the user with email and password async.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="onDone">On done.</param>
        public void CreateUserWithEmailAndPasswordAsync(string email, string password, System.Action<LoginResult> onDone)
        {
            Debug.Log(string.Format("Attempting to create user {0}...", email));
            auth.CreateUserWithEmailAndPasswordAsync(email, password)
                .ContinueWith((authTask) =>
                {
                    if (authTask.IsCanceled)
                    {
                        Debug.Log("CreateUserWithEmailAndPasswordAsync canceled.");
                        if (onDone != null)
                            onDone(LoginResult.CANCELLED);
                    }
                    else if (authTask.IsFaulted)
                    {
                        Debug.Log("CreateUserWithEmailAndPasswordAsync encounted an error.");
                        Debug.LogError(authTask.Exception.ToString());
                        if (onDone != null)
                            onDone(LoginResult.ERROR);
                    }
                    else if (authTask.IsCompleted)
                    {
                        Debug.Log("CreateUserWithEmailAndPasswordAsync completed");
//                        UpdateUserProfile(newDisplayName: newDisplayName);
//                        Firebase.Auth.FirebaseUser newUser = authTask.Result;
                        if (onDone != null)
                            onDone(LoginResult.OK);
                    }
                });
        }

        /// <summary>
        /// Signs the in with email and password async.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="onDone">On done.</param>
        public void SignInWithEmailAndPasswordAsync(string email, string password, System.Action<LoginResult> onDone)
        {
            Debug.Log(string.Format("Attempting to login user {0}...", email));
            auth.SignInWithEmailAndPasswordAsync(email, password)
                .ContinueWith((authTask) =>
                {
                    if (authTask.IsCanceled)
                    {
                        Debug.Log("SignInWithEmailAndPasswordAsync canceled.");
                        if (onDone != null)
                            onDone(LoginResult.CANCELLED);
                    }
                    else if (authTask.IsFaulted)
                    {
                        Debug.Log("SignInWithEmailAndPasswordAsync encounted an error.");
                        Debug.LogError(authTask.Exception.ToString());
                        if (onDone != null)
                            onDone(LoginResult.ERROR);
                    }
                    else if (authTask.IsCompleted)
                    {
                        Debug.Log("SignInWithEmailAndPasswordAsync completed");
//                        UpdateUserProfile(newDisplayName: newDisplayName);
//                        Firebase.Auth.FirebaseUser newUser = authTask.Result;
                        if (onDone != null)
                            onDone(LoginResult.OK);
                    }
                });
        }

        //    // Update the user's display name with the currently selected display name.
        //    public void UpdateUserProfile(string newDisplayName = null) {
        //        if (user == null) {
        //            DebugLog("Not signed in, unable to update user profile");
        //            return;
        //        }
        //        displayName = newDisplayName ?? displayName;
        //        DebugLog("Updating user profile");
        //        DisableUI();
        //        user.UpdateUserProfileAsync(new Firebase.Auth.UserProfile {
        //            DisplayName = displayName,
        //            PhotoUrl = user.PhotoUrl,
        //        }).ContinueWith(HandleUpdateUserProfile);
        //    }

        public ILoginData LoginData
        {
            get
            {
                return loginData;
            }
        }

        // Display user information.
        void DisplayUserInfo(Firebase.Auth.IUserInfo userInfo, int indentLevel)
        {
            string indent = new string(' ', indentLevel * 2);
            var userProperties = new Dictionary<string, string>
            {
                { "Display Name", userInfo.DisplayName },
                { "Email", userInfo.Email },
                { "Photo URL", userInfo.PhotoUrl != null ? userInfo.PhotoUrl.ToString() : null },
                { "Provider ID", userInfo.ProviderId },
                { "User ID", userInfo.UserId }
            };
            foreach (var property in userProperties)
            {
                if (!string.IsNullOrEmpty(property.Value))
                {
                    Debug.Log(string.Format("{0}{1}: {2}", indent, property.Key, property.Value));
                }
            }
        }

        // Track state changes of the auth object.
        void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (auth.CurrentUser != user)
            {
                bool signedIn = IsSignedIn();
                if (!signedIn && user != null)
                {
                    Debug.Log("Signed out " + user.UserId);
                }
                user = auth.CurrentUser;
                if (loginData == null)
                    loginData = new QuizHeroLoginData(user);
                else
                    loginData.User = user;
                

                if (signedIn)
                {
                    Debug.Log("Signed in " + user.UserId);
//                displayName = user.DisplayName ?? "";
                    DisplayUserInfo(user, 1);
                    Debug.Log("  Anonymous: " + user.IsAnonymous);
                    Debug.Log("  Email Verified: " + user.IsEmailVerified);
                    var providerDataList = new List<Firebase.Auth.IUserInfo>(user.ProviderData);
                    if (providerDataList.Count > 0)
                    {
                        Debug.Log("  Provider Data:");
                        foreach (var providerData in user.ProviderData)
                        {
                            DisplayUserInfo(providerData, 2);
                        }
                    }
                }

                authStateChangedEvent();
            }
        }

        private event AuthStateChangedEvent authStateChangedEvent = delegate
        {
        };

        public void RegisterAuthStateChangedListener(AuthStateChangedEvent listener)
        {
            authStateChangedEvent += listener;
        }

        public void UnregisterAuthStateChangedListener(AuthStateChangedEvent listener)
        {
            authStateChangedEvent -= listener;
        }
    }
}