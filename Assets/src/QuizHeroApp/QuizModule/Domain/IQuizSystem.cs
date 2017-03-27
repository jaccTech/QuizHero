using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    public interface IQuizSystem
    {
        void FetchAvailableQuizzes(System.Action<List<QuizVO>> onDoneCallback);

        void UpdateSelectedQuiz(QuizVO quiz, System.Action onDoneCallback);

        void FetchCurrentStage(System.Action<StageVO> onDoneCallback);

        void RegisterCurrentStageValueChangedListener(System.Action<StageVO> onDoneCallback);

        void SubmitAnswer(long questionId, AnswerVO answer, System.Action onDoneCallback);
    }
}