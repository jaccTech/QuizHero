using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    [System.Serializable]
    public class QuestionVO : IQuestionVO
    {
        public string question;
        public List<string> options;
        public bool isMultiselection;

        public string Question { get { return question; } }
        public List<string> Options { get { return options; } }
        public bool IsMultiselection { get { return isMultiselection; } }
    }
}

