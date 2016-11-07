﻿using System;
using System.Collections.Generic;

namespace knatteligan.Domain.Entities
{
    public class Match
    {
        public Guid Id { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public DateTime MatchDate { get; set; }

        public Match(Team homeTeam, Team awayTeam)
        {
            Id = new Guid();
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public Match()
        {

        }
    }
}