using System;

namespace knatteligan.Domain.ValueObjects {

    public class InvalidMatchDateException : Exception {

        public InvalidMatchDateException(string matchDateIsNotValid):base(matchDateIsNotValid) {}

    }

}