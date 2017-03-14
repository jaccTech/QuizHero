using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


namespace com.xavi.QuizGame.HomeModule.Presentation
{
    public class HomeSceneView : MonoBehaviour
    {
        void Start()
        {
            Debug.Log("HomeSceneView.Start");
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://quizgame-ba103.firebaseio.com ");

            // Get the root reference location of the database.
            DatabaseReference leaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("Leaderboard");

            leaderboardReference.GetValueAsync().ContinueWith(
                (task) =>
                {
                    if (task.IsFaulted)
                    {
                        // Handle the error...
                        Debug.LogError(task.Exception.ToString());
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        // Do something with snapshot...
                        Debug.Log("Got Leaderboard " + snapshot.ToString());
                    }
                });
        }
    }
}
