using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using knatteligan.Domain.Entities;

namespace knatteligan.Helpers
{
    public class MatchWeek
    {
        
        public List<Guid> Matches { get; set; }
        public List<Guid> SuspendedPlayers { get; set; }

    }
}
