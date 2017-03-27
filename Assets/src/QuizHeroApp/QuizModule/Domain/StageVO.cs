namespace com.xavi.QuizHero.QuizModule.Domain
{
    [System.Serializable]
    public class StageVO
    {
        public long level;
        public int state;
        public double points;
        public QuestionVO currentQuestion;
    }
}