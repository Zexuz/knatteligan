using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knatteligan.Domain.Entities
{
   public class Assist
    {
        public Guid PlayerGuid { get; set; }
        public Guid MatchGuid { get; set; }
        public Guid Id { get; set; }

        public Assist(Guid playerGuid, Guid matchGuid)
        {
            Id = Guid.NewGuid();
            PlayerGuid = playerGuid;
            MatchGuid = matchGuid;
        }
        public Assist()
        {

        }
    }
}
