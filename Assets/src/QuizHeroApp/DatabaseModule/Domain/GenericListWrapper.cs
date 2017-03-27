using System;
using System.Collections.Generic;

namespace com.xavi.QuizHero.DatabaseModule.Domain
{
    [Serializable]
    public class GenericListWrapper<T>
    {
        public List<T> dataList;
    }
}

