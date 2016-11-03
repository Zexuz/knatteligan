using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities {

    public class Coach : TeamPerson {

        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        public Coach() {}

        public Coach(PersonName name, PersonalNumber personalNumber, PhoneNumber phoneNumber, Email email, Team team) : base(name, personalNumber, team) {
            PhoneNumber = phoneNumber;
            Email = email;
        }

    }
}