using System;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Coach : TeamPerson
    {
        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        public Coach(PersonName name, PersonalId personId, PhoneNumber phoneNumber, Email email) : base(name, personId)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}