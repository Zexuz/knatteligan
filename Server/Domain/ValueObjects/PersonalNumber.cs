﻿using System;
using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class PersonalNumber
    {
        public DateTime DateOfBirth { get; set; }
        public string LastFour { get; set; }

        public PersonalNumber() { }

        public PersonalNumber(DateTime dateOfBirth, string lastFour)
        {
            if (!IsValid(dateOfBirth, lastFour))
                throw new InvalidPersonalIdException("The personal id is not valid");

            DateOfBirth = dateOfBirth;
            LastFour = lastFour;
        }

        public static bool IsValid(DateTime dateTime, string lastFour)
        {
            var doa = dateTime.ToString("yyyy-MM-dd");
            var personalId = $"{doa}-{lastFour}";
            var regEx = new Regex(@"\d{4}-\d{2}-\d{2}-\d{4}");

            var isValidDate = DateTime.Now > dateTime && dateTime > new DateTime(1899,11,19);
            return regEx.IsMatch(personalId) && isValidDate;
        }

        public override string ToString()
        {
            return $"{DateOfBirth:yyMMdd}-{LastFour}";
        }
    }
}