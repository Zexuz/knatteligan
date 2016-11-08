using System;

namespace knatteligan.Domain.ValueObjects {

    public class InvalidTeamNameException : Exception {

        public InvalidTeamNameException(string badName) : base(badName) {}

    }

}