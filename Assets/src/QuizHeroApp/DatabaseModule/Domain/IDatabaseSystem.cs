using System.Collections.Generic;


namespace com.xavi.QuizHero.DatabaseModule.Domain
{
    public static class DatabaseConstants
    {
        public const string DatabaseURL = "https://quizgame-ba103.firebaseio.com";
    }

    public delegate void ModelUpdatedEvent<T>(T model);

    public interface IDatabaseSystem
    {
        void GetListValueAsync<T>(string referencePath, ModelUpdatedEvent<List<T>> modelUpdatedEvent);

        void GetValueAsync<T>(string referencePath, ModelUpdatedEvent<T> modelUpdatedEvent);

        void RegisterValueChangedListener<T>(string referencePath, System.Action<T> onDoneCallback);

        void SetRawJsonValueAsync (string referencePath, string value, System.Action onDoneCallback);
    }
}

