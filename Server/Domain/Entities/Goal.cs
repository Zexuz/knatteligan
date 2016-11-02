using System;

namespace knatteligan.Domain.Entities
{
    public class Goal
    {
        public Guid guidPlayer { get; set; }
        public Guid guidTeam { get; set; }
        public Guid guid { get; set; }
    }
}
