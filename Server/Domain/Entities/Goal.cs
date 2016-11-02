using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knatteligan.Domain.Entities
{
    public class Goal
    {
        public Guid guidPlayer { get; set; }
        public Guid guidTeam { get; set; }
        public Guid guid { get; set; }

        
    }
}
