using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    public interface IQuizSystem
    {
        void FetchAvailableQuizzes(System.Action<List<QuizVO>> onDoneCallback);

        void UpdateSelectedQuiz(QuizVO quiz, System.Action onDoneCallback);

        void FetchCurrentQuestion(System.Action<QuestionVO> onDoneCallback);

        void AddCurrentQuestionValueChangedListener(System.Action<QuestionVO> onDoneCallback);

        void SubmitAnswer(AnswerVO answer, System.Action onDoneCallback);
    }
}