using System;
using System.Collections.Generic;
using System.Linq;

namespace knatteligan.Helpers {

    public class CreateSeriesScheual {

        public Dictionary<int, List<Match>> GetFullSeries() {
            var list = new List<string>();
            list.Add("Team 1");
            list.Add("Team 2");
            list.Add("Team 3");
            list.Add("Team 4");

            var firstHalf = ListMatches(list, false);
            for (int i = 0; i < firstHalf.Count; i+=2) {
                firstHalf[i + 1][0].Swap();
            }


            var secondHalf = ListMatches(list, true);
            for (int i = 0; i < secondHalf.Count; i+=2) {
                secondHalf[i + 1][0].Swap();
            }


            var wholeSeries = firstHalf.ToDictionary(entry => entry.Key,
                entry => entry.Value);

            for (int i = firstHalf.Count + 1; i < firstHalf.Count * 2+1; i++) {
                wholeSeries.Add(i, secondHalf[i - firstHalf.Count]);
            }


            foreach (var round in wholeSeries) {
                Console.WriteLine($"--- Round {round.Key}---");
                foreach (var match in round.Value) {
                    Console.WriteLine(match);
                }
            }

            return wholeSeries;
        }


        public Dictionary<int, List<Match>> ListMatches(List<string> listTeam, bool revert) {
            if (listTeam.Count % 2 != 0) {
                listTeam.Add("Bye");
            }

            int numDays = (listTeam.Count - 1);
            int halfSize = listTeam.Count / 2;

            List<string> teams = new List<string>();

            teams.AddRange(listTeam.Skip(halfSize).Take(halfSize));
            teams.AddRange(listTeam.Skip(1).Take(halfSize - 1).ToArray().Reverse());

            int teamsSize = teams.Count;

            var rounds = new Dictionary<int, List<Match>>();

            for (int day = 0; day < numDays; day++) {
                rounds.Add(day + 1, new List<Match>());

                int teamIdx = day % teamsSize;
                var match = new Match();
                match.Away = teams[teamIdx];
                match.Home = listTeam[0];
                rounds[day + 1].Add(match);
                if (revert)
                    rounds[day + 1][0].Swap();


                for (int idx = 1; idx < halfSize; idx++) {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;

                    var newMathc = new Match();
                    newMathc.Away = teams[firstTeam];
                    newMathc.Home = teams[secondTeam];
                    rounds[day + 1].Add(newMathc);
                    if (revert)
                        rounds[day + 1][rounds[day + 1].Count - 1].Swap();
                }
            }

            return rounds;
        }

    }

    public class Match {

        public string Away { get; set; }
        public string Home { get; set; }


        public void Swap() {
            var temp = Away;
            Away = Home;
            Home = temp;
        }

        public override string ToString() {
            return $"Home team {Home} - Away team {Away}";
        }

    }

}

}