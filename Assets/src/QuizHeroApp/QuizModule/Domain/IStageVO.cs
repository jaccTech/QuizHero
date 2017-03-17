namespace com.xavi.QuizHero.QuizModule.Domain
{
    public interface IStageVO
    {
        long Level { get; }
        long QuizId { get; }
        IQuizVO QuizVO { get; }
    }
}