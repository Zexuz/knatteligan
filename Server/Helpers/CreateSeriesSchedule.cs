using System;
using System.Collections.Generic;
using System.Linq;

using knatteligan.CustomExceptions;
using knatteligan.Domain.Entities;

namespace knatteligan.Helpers
{

    public class CreateSeriesSchedule
    {

        public Dictionary<int, List<Match>> GetFullSeries(List<Team> teams)
        {
            var firstHalf = ListMatches(teams, false);
            SwapAllEvenMatchesAtIndexZero(firstHalf);

            var secondHalf = ListMatches(teams, true);
            SwapAllEvenMatchesAtIndexZero(secondHalf);

            var wholeSeries = firstHalf.ToDictionary(entry => entry.Key, entry => entry.Value);

            //adds the second half to the first half
            for (int i = firstHalf.Count + 1; i < firstHalf.Count * 2 + 1; i++) {
                wholeSeries.Add(i, secondHalf[i - firstHalf.Count]);
            }


            return wholeSeries;
        }

        public void PrintMatches(Dictionary<int, List<Match>> wholeSeries) {
            foreach (var round in wholeSeries) {
                Console.WriteLine($"--- Round {round.Key}---");
                foreach (var match in round.Value) {
                    Console.WriteLine(match);
                }
            }
        }

        //used to swap all even matches in every first group at index 0 EG team nr 1
        private void SwapAllEvenMatchesAtIndexZero(Dictionary<int, List<Match>> dictionary) {
            for (int i = 0; i < dictionary.Count; i += 2) {
                dictionary[i + 1][0].Swap();
            }
        }


        private Dictionary<int, List<Match>> ListMatches(List<Team> listTeam, bool revert) {
            if (listTeam.Count % 2 != 0) {
                throw new InvalidNumberOfTeamsException("There must be a even nunmber of teams to do this!");
            }

            var numRounds = listTeam.Count - 1;
            var halfSize = listTeam.Count / 2;

            var teams = new List<Team>();

            teams.AddRange(listTeam.Skip(halfSize).Take(halfSize));
            teams.AddRange(listTeam.Skip(1).Take(halfSize - 1).ToArray().Reverse());

            int teamsSize = teams.Count;

            var rounds = new Dictionary<int, List<Match>>();

            for (int round = 0; round < numRounds; round++) {
                var currentRoundNr = round + 1; // round starts @ index 0, our rounds start at 1

                rounds.Add(currentRoundNr, new List<Match>());

                var currentRound = rounds[currentRoundNr];

                int teamIdx = round % teamsSize;

                var match = new Match {
                    AwayTeam = teams[teamIdx],
                    HomeTeam = listTeam[0]
                };

                currentRound.Add(match);

                if (revert)
                    currentRound[0].Swap();


                for (int idx = 1; idx < halfSize; idx++) {
                    int firstTeam = (round + idx) % teamsSize;
                    int secondTeam = (round + teamsSize - idx) % teamsSize;

                    var newMatch = new Match {
                        AwayTeam = teams[firstTeam],
                        HomeTeam = teams[secondTeam]
                    };
                    currentRound.Add(newMatch);

                    if (revert)
                        currentRound[currentRound.Count - 1].Swap();
                }
            }

            return rounds;
        }
    }
}