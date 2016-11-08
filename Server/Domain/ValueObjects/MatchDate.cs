using System;

namespace knatteligan.Domain.ValueObjects
{
    public class MatchDate
    {

        public DateTime Value  { get; set; }
        public MatchDate(DateTime matchDate, DateTime leagueStartTime, DateTime endOfLeague)
        {
            if (!IsValid(matchDate, leagueStartTime, endOfLeague))
            {
                throw new InvalidMatchDateException("Match date is not valid");
            }
            Value = matchDate;
            
        }

        private bool IsValid(DateTime matchDate, DateTime leagueStartTime, DateTime endOfLeague)
        {           
            return matchDate > leagueStartTime && matchDate < endOfLeague;
        }

        public override string ToString() {
            return Value.ToString("D");
        }

    }

}