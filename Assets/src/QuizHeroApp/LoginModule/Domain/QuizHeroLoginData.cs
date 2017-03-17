using System;
using com.xavi.LoginModule.Domain;
using Firebase.Auth;

namespace com.xavi.QuizHero.Domain.LoginSystem
{
    public class QuizHeroLoginData : ILoginData
    {
        public FirebaseUser User;

        public QuizHeroLoginData(FirebaseUser user)
        {
            this.User = user;
        }

        public string UserId { get { return User.UserId; } }
    }
}

