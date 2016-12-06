using System;
using System.Collections.Generic;

namespace knatteligan.Helpers
{
    public class MatchWeek
    {
        public List<Guid> MatchIds { get; set; }
        public HashSet<Guid> SuspendedPlayerIds { get; set; }

        public MatchWeek()
        {
            MatchIds = new List<Guid>();
            SuspendedPlayerIds = new HashSet<Guid>();
        }
    }
}