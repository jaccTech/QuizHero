using System;
using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    public class AnswerVO : IAnswerVO
    {
        public AnswerVO(List<int> answerList)
        {
            this.answer = answerList;
        }

        public List<int> answer;

        public List<int> Answer { get { return answer; } }
    }
}

