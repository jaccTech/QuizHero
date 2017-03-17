
namespace com.xavi.QuizHero.QuizModule.Domain
{
    [System.Serializable]
    public class StageVO : IStageVO
    {
        public long level;
        public long quiz;
        public IQuizVO quizVO;

        public long Level { get { return level; } }
        public long QuizId { get { return quiz; } }
        public IQuizVO QuizVO { get { return quizVO; } }
    }
}