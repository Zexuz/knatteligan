﻿using System;
using System.Collections.Generic;
using System.Linq;
using knatteligan.CustomExceptions;
using knatteligan.Domain.Entities;
using knatteligan.Services;

namespace knatteligan.Helpers
{
    public class CreateSeriesSchedule
    {
        private readonly MatchService _matchService = new MatchService();

        public SerializableDictionary<int, MatchWeek> GetFullSeries(List<Team> teams)
        {
            var firstHalf = ListMatches(teams, false);
            SwapAllEvenMatchesAtIndexZero(firstHalf);

            var secondHalf = ListMatches(teams, true);
            SwapAllEvenMatchesAtIndexZero(secondHalf);

            //adds the second half to the first half
            var firstHalfCount = firstHalf.Count;
            for (int i = firstHalfCount + 1; i < firstHalfCount * 2 + 1; i++)
            {
                firstHalf.Add(i, secondHalf[i - firstHalfCount]);
            }

            return firstHalf;
        }

        public void PrintMatches(SerializableDictionary<int, MatchWeek> wholeSeries)
        {
            foreach (var round in wholeSeries)
            {
                Console.WriteLine($"--- Round {round.Key}---");
                foreach (var match in round.Value.MatchIds)
                {
                    Console.WriteLine(_matchService.FindById(match));
                }
            }
        }

        //used to swap all even matches in every first group at index 0 EG team nr 1
        private void SwapAllEvenMatchesAtIndexZero(SerializableDictionary<int, MatchWeek> dictionary)
        {
            for (int i = 0; i < dictionary.Count; i += 2)
            {
                var matchId = dictionary[i + 1].MatchIds[0];
                var match = _matchService.FindById(matchId);
                match.Swap();
                _matchService.Save();
            }
        }

        private SerializableDictionary<int, MatchWeek> ListMatches(List<Team> listTeam, bool revert)
        {
            if (listTeam.Count % 2 != 0)
            {
                throw new InvalidNumberOfTeamsException("There must be a even nunmber of teams to do this!");
            }

            var numRounds = listTeam.Count - 1;
            var halfSize = listTeam.Count / 2;

            var teams = new List<Team>();

            teams.AddRange(listTeam.Skip(halfSize).Take(halfSize));
            teams.AddRange(listTeam.Skip(1).Take(halfSize - 1).ToArray().Reverse());

            int teamsSize = teams.Count;

            var rounds = new SerializableDictionary<int, MatchWeek>();

            for (int round = 0; round < numRounds; round++)
            {
                var currentRoundNr = round + 1; // round starts @ index 0, our rounds start at 1

                rounds.Add(currentRoundNr, new MatchWeek());

                var currentRound = rounds[currentRoundNr];

                int teamIdx = round % teamsSize;

                var match = new Match
                {
                    AwayTeamId = teams[teamIdx].Id,
                    HomeTeamId = listTeam[0].Id
                };

                currentRound.MatchIds.Add(match.Id);

                if (revert)
                    match.Swap();

                _matchService.Add(match);


                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (round + idx) % teamsSize;
                    int secondTeam = (round + teamsSize - idx) % teamsSize;

                    var newMatch = new Match
                    {
                        AwayTeamId = teams[firstTeam].Id,
                        HomeTeamId = teams[secondTeam].Id
                    };
                    currentRound.MatchIds.Add(newMatch.Id);

                    if (revert)
                        newMatch.Swap();

                    _matchService.Add(newMatch);

                }
            }

            return rounds;
        }
    }
}