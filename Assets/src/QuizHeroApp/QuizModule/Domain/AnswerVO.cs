using System;
using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    [Serializable]
    public class AnswerVO
    {
        public List<int> answer;
        public float time;

        public AnswerVO(List<int> answerList, float time)
        {
            this.answer = answerList;
            this.time = time;
        }
    }
}

