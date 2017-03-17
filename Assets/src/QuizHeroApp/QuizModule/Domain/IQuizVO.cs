using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    public interface IQuizVO
    {
        string Question { get; }
        List<string> Options { get; }
    }
}