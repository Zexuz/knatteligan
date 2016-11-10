using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities {

    public class Coach : Person {

        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }


        public Coach() {}

        public Coach(PersonName name, PersonalNumber personalNumber, PhoneNumber phoneNumber, Email email) : base(name, personalNumber) {
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public override Persons GetType()
        {
            return Persons.Coach;
        }

    }
}