using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    [System.Serializable]
    public class QuestionVO
    {
        public long id;
        public bool isMultiselection;
        public long level;
        public List<string> options;
        public long points;
        public string question;
        public long time;
    }
}

