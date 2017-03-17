using com.xavi.QuizHero.QuizModule.Domain;

namespace com.xavi.QuizHero.QuizModule.Application
{
    public class QuizApp : IQuizApp
    {
        private IQuizSystem _quizSystem;

        private IStageVO stage;

        public QuizApp (IQuizSystem quizSystem)
        {
            this._quizSystem = quizSystem;
        }

        public IStageVO Stage { get { return stage; } }

        public void FetchCurrentStage (System.Action onDone)
        {
            this._quizSystem.FetchCurrentStage((IStageVO stage) =>
                {
                    this.stage = stage;
                    if (onDone != null)
                        onDone();
                });
        }
    }
}
