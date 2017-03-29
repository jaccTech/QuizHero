using System;
using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    [Serializable]
    public class AnswerVO
    {
        public long questionId;
        public List<int> answer;
        public float time;

        public AnswerVO(long questionId, List<int> answerList, float time)
        {
            this.questionId = questionId;
            this.answer = answerList;
            this.time = time;
        }
    }
}

