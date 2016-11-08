using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knatteligan.Domain.ValueObjects
{
    class MatchDate
    {

        public DateTime Value  { get; set; }
        public MatchDate(DateTime matchDate, DateTime leagueStartTime, DateTime endOfLeague)
        {
            if (!IsValid(matchDate, leagueStartTime, endOfLeague))
            {
                throw new Exception("Match date is not valid");
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