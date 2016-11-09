using System;
using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class PersonName
    {
        public string Name { get; set; }      

        public PersonName() {

        }

        public PersonName(string name)
        {
            if (!IsName(name))
                throw new InvalidPersonNameException("Bad name.");

            Name = name;

        }

        private static bool IsName(string name)
        {
            const string regExString =
                "^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]{2,30}$";
            return Regex.IsMatch(name, regExString);
        }

        public override string ToString() {
            return $"{Name}";
        }

    }
}