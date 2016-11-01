using System;
using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class PersonalId
    {
        public DateTime DateOfBirth { get; set; }
        public string LastFour { get; set; }

        public PersonalId(DateTime dateOfBirth, string lastFour)
        {
            if (!IsValid(dateOfBirth, lastFour))
            {
                throw new InvalidPersonalIdException("The personal id is not valid");
            }

            DateOfBirth = dateOfBirth;
            LastFour = lastFour;
        }

        public static bool IsValid(DateTime dateTime, string lastFour)
        {
            //todo check that datetime is a valid time eg, not in furure
            var doa = dateTime.ToString("yyyy-MM-dd");
            var personalId = $"{doa}-{lastFour}";
            var regEx = new Regex(@"\d{4}-\d{2}-\d{2}-\d{4}");

            var isValidDate = DateTime.Now > dateTime;

            return regEx.IsMatch(personalId) && isValidDate;
        }
    }
}