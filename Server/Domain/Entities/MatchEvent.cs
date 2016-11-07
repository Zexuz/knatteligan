using System;

namespace knatteligan.Domain.Entities
{
    public abstract class MatchEvent : Entity
    {
        public abstract Guid PlayerGuid { get; set; }
        public abstract Guid MatchGuid { get; set; }


        public new abstract MatchEvents GetType();
    }


    public enum MatchEvents
    {
        RedCard,
        YellowCard,
        Assist,
        Goal
    }
}