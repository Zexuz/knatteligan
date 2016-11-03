using System;

namespace knatteligan.CustomExceptions {

    public class InvalidNumberOfTeamsException : Exception {

        public InvalidNumberOfTeamsException(string str) : base(str) {}

    }

}