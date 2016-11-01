using System;
using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    class PersonName 
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

        public PersonName()
        {

        }

        public static bool IsName(string firstName, string lastName)
        {
            return Regex.IsMatch(firstName, "^[a - zA - ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]{2,30}$")
                && Regex.IsMatch(lastName, "^[a - zA - ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð,.'-]{2,30}$");
        }
        
    }
}
