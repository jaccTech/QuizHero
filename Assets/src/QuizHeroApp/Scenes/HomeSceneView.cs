using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.xavi.QuizHero.Domain.LoginSystem;


namespace com.xavi.QuizHero.HomeModule.Presentation
{
    public class HomeSceneView : MonoBehaviour
    {
        [SerializeField] private Button playButton;

        void Start()
        {
            Debug.Log("HomeSceneView.Start");
//            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://quizgame-ba103.firebaseio.com ");
//
//            // Get the root reference location of the database.
//            DatabaseReference leaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
//
//            leaderboardReference.GetValueAsync().ContinueWith(
//                (task) =>
//                {
//                    if (task.IsFaulted)
//                    {
//                        // Handle the error...
//                        Debug.LogError(task.Exception.ToString());
//                    }
//                    else if (task.IsCompleted)
//                    {
//                        DataSnapshot snapshot = task.Result;
//                        // Do something with snapshot...
//                        Debug.Log("Got Leaderboard " + snapshot.ToString());
//                    }
//                });
//
//            FirebaseDatabase.DefaultInstance
//                .GetReference("Leaders")
//                .GetValueAsync().ContinueWith(task => {
//                    if (task.IsFaulted) {
//                        // Handle the error...
//                    }
//                    else if (task.IsCompleted) {
//                        DataSnapshot snapshot = task.Result;
//                        // Do something with snapshot...
//                    }
//                });
            
            playButton.onClick.AddListener(HandlePlayButtonClick);
        }

        private void HandlePlayButtonClick()
        {
            SceneManager.LoadSceneAsync(SceneConstanst.SceneName.GameScene, LoadSceneMode.Single);
        }
    }
}
