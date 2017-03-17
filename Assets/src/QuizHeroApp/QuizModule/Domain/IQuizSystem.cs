﻿
namespace com.xavi.QuizHero.QuizModule.Domain
{
    public delegate void QuizUpdatedEvent(IStageVO quiz);

    public interface IQuizSystem
    {
        void FetchCurrentStage(QuizUpdatedEvent quiz);
    }
}