using System;

namespace knatteligan.Domain.Entities
{
    public class Goal
    {
        public Guid PlayerGuid { get; set; }
        public Guid TeamGuid { get; set; }
        public Guid Id { get; set; }

        public Goal(Guid playerGuid, Guid teamGuid)
        {
            Id = Guid.NewGuid();
            PlayerGuid = playerGuid;
            TeamGuid = teamGuid;

        }
        public Goal()
        {

        }
    }
}
