using com.xavi.QuizHero.QuizModule.Domain;
using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Application
{
    public interface IQuizApp
    {
        IStageVO Stage { get; }

        void FetchCurrentStage(System.Action onDone);

        void SubmitAnswer(IAnswerVO answer, System.Action onDone);
    }
}