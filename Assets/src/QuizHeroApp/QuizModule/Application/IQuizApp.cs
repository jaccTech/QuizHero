using com.xavi.QuizHero.QuizModule.Domain;

namespace com.xavi.QuizHero.QuizModule.Application
{
    public interface IQuizApp
    {
        IStageVO Stage { get; }

        void FetchCurrentStage(System.Action onDone);
    }
}