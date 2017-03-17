﻿using System.Collections.Generic;

namespace com.xavi.QuizHero.QuizModule.Domain
{
    [System.Serializable]
    public class QuizVO : IQuizVO
    {
        public string question;
        public List<string> options;

        public string Question { get { return question; } }
        public List<string> Options { get { return options; } }
    }
}
