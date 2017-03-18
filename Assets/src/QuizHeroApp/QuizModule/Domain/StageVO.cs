
namespace com.xavi.QuizHero.QuizModule.Domain
{
    [System.Serializable]
    public class StageVO : IStageVO
    {
        public long level;
        public long question;
        public IQuestionVO questionVO;

        public long Level { get { return level; } }
        public long QuestionId { get { return question; } }
        public IQuestionVO QuestionVO { get { return questionVO; } }
    }
}