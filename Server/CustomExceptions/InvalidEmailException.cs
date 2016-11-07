using System;

namespace knatteligan.CustomExceptions {

    public class InvalidEmailException : Exception {

        public InvalidEmailException(string message) : base(message) {}

    }

}