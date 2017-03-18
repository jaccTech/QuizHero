using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    public interface IQuestionVO
    {
        string Question { get; }
        List<string> Options { get; }
        bool IsMultiselection { get; }
    }
}