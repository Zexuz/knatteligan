using System;

namespace knatteligan.Domain.ValueObjects {

    public class InvalidPhoneNumberException : Exception {

        public InvalidPhoneNumberException(string badPhoneNumber):base(badPhoneNumber) {}

    }

}