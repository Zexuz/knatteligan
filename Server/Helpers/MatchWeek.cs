using System;
using System.Collections.Generic;

namespace knatteligan.Helpers
{
    public class MatchWeek
    {
        public List<Guid> Matches { get; set; }
        public List<Guid> SuspendedPlayers { get; set; }

        public MatchWeek()
        {
            Matches = new List<Guid>();
            SuspendedPlayers = new List<Guid>();
        }
    }
}