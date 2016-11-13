﻿using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class TeamService
    {
        private readonly TeamRepository _teamRepository;

        public TeamService()
        {
            _teamRepository = TeamRepository.GetInstance();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _teamRepository.GetAll();
        }

        public void Add(Team team)
        {
            _teamRepository.Add(team);
        }

        public void RemoveTeam(Team team)
        {
            throw new NotImplementedException();
        }

        public void ChangeTeamName(Team team, TeamName newName)
        {
            team.Name = newName;
        }

        public Team FindById(Guid teamId)
        {
            return _teamRepository.FindBy(teamId);
        }
    }
}
