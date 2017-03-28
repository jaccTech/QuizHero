using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using System.Collections.Generic;

namespace com.xavi.QuizHero.DatabaseModule.Domain
{
    public class FirebaseDatabaseSystem : IDatabaseSystem
    {
        public FirebaseDatabaseSystem()
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DatabaseConstants.DatabaseURL);
        }

        public void GetListValueAsync<T>(string referencePath, System.Action<List<T>> modelUpdatedEvent)
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

                        if (snapshot != null)
                        {
                            string newJson = "{\"dataList\": " + snapshot.GetRawJsonValue() + "}";
                            GenericListWrapper<T> listWrapper = JsonUtility.FromJson<GenericListWrapper<T>>(newJson);
                            modelUpdatedEvent(listWrapper.dataList);
                        }
                        else
                        {
                            modelUpdatedEvent(new List<T>());
                        }
                    }
                    else if (task.IsCanceled)
                    {
                        Debug.Log("GetListValueAsync Cancelled");
                    }
                });
        }

        public void GetValueAsync<T>(string referencePath, System.Action<T> modelUpdatedEvent)
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
                        Debug.Log("snapshot.GetRawJsonValue: " + snapshot.GetRawJsonValue());
                        modelUpdatedEvent(snapshot != null ? JsonUtility.FromJson<T>(snapshot.GetRawJsonValue()) : default(T));
                    }
                    else if (task.IsCanceled)
                    {
                        // Handle the error...
                        Debug.LogWarning("GetValueAsync cancelled.");
                        modelUpdatedEvent(default(T));
                    }
                });
        }

        public void RegisterValueChangedListener<T>(string referencePath, System.Action<T> onDoneCallback)
        {
            // get the quiz
            FirebaseDatabase.DefaultInstance
                .GetReference(referencePath).ValueChanged += delegate (object sender, ValueChangedEventArgs args)
            {
                Debug.Log(string.Format("ValueChanged {0} {1}", sender, args));
                if (args.DatabaseError != null)
                {
                    Debug.LogError(args.DatabaseError.Message);
                    return;
                }
                
                // Do something with the data in args.Snapshot
                if (args.Snapshot != null)
                {
                    Debug.Log("args.Snapshot.GetRawJsonValue(): " + args.Snapshot.GetRawJsonValue());
                    onDoneCallback(args.Snapshot != null ? JsonUtility.FromJson<T>(args.Snapshot.GetRawJsonValue()) : default(T));    
                }
            };
        }

        public void SetRawJsonValueAsync(string referencePath, string value, System.Action onDoneCallback)
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