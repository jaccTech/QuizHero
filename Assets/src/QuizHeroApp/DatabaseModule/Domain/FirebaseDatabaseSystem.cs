using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

namespace com.xavi.QuizHero.DatabaseModule.Domain
{
    public class FirebaseDatabaseSystem : IDatabaseSystem
    {
        public FirebaseDatabaseSystem()
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DatabaseConstants.DatabaseURL);
        }

        public void GetValueAsync<T>(string referencePath, ModelUpdatedEvent<T> modelUpdatedEvent)
        {
            // get the quiz
            FirebaseDatabase.DefaultInstance
                .GetReference(referencePath)
                .GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        // Handle the error...
                        Debug.LogError(task.Exception.ToString());
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        modelUpdatedEvent(snapshot != null ? JsonUtility.FromJson<T>(snapshot.GetRawJsonValue()) : default(T));
                    }
                });
        }
    }
}