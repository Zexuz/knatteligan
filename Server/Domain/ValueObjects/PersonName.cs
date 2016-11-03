using System;
using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class PersonName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PersonName(string firstName, string lastName)
        {
            if (!IsName(firstName, lastName))
                throw new Exception("Bad name.");

            FirstName = firstName;
            LastName = lastName;
        }

        private static bool IsName(string firstName, string lastName)
        {
            const string regExString =
                "^[a - zA - ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]{2,30}$";
            return Regex.IsMatch(firstName, regExString) && Regex.IsMatch(lastName, regExString);
        }
    }
}