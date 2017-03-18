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

        public void SetRawJsonValueAsync (string referencePath, string value, System.Action onDoneCallback)
        {
            Debug.Log(string.Format("Setting {0} to {1}", referencePath, value));
            FirebaseDatabase.DefaultInstance
                .GetReference(referencePath)
                .SetRawJsonValueAsync(value).ContinueWith(task =>
                {
                        if (task.IsFaulted)
                        {
                            // Handle the error...
                            Debug.LogError(task.Exception.ToString());
                        }
                        else if (task.IsCompleted)
                        {
                            if (onDoneCallback != null)
                                onDoneCallback();
                        }    
                });
        }
    }
}