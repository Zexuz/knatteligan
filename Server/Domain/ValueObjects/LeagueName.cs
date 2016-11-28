using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class LeagueName
    {
        public string Value { get; set; }

        public LeagueName() { }

        public LeagueName(string name)
        {
            if (!IsLeagueName(name))
                throw new InvalidTeamNameException("Bad name.");

            Value = name;
        }

        private static bool IsLeagueName(string name)
        {
            return Regex.IsMatch(name, "^[a-ö\\sA-ö1-9àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð-]{2,30}$");
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
