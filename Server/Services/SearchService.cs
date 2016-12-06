using knatteligan.Repositories;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Helpers;
using static knatteligan.PlayerSearchResultItem;

namespace knatteligan.Services
{
    public class SearchService
    {
        public IEnumerable<SearchResultItem> Search(string freeText, bool ignoreCase)
        {
            IEnumerable<SearchResultItem> leagueResult = LeagueRepo.GetAll().Where(l => l.Name.Value.Contains(freeText, ignoreCase)).Select(l => new LeagueSearchResultItem(l));
            //|| m.ProductionYear.Value.ToString().Contains(freeText, ignoreCase)
            // ).Select(m => new LeagueSearchResultItem(m));

            IEnumerable<SearchResultItem> teamResult = TeamRepo.GetAll().Where(t => t.Name.Value.Contains(freeText, ignoreCase)).Select(t => new TeamSearchResultItem(t));

            IEnumerable<SearchResultItem> playerResult = PersonRepo.GetAllPlayers().Where(p => p.Name.Name.Contains(freeText, ignoreCase)).Select(p => new PlayerSearchResultItem(p));

            IEnumerable<SearchResultItem> playerPersonalNumber = PersonRepo.GetAllPlayers().Where(p => p.PersonalNumber.ToString().Contains(freeText, ignoreCase)).Select(p => new PlayerSearchResultItem(p));
            //IEnumerable<SearchResultItem> playerResult = PersonRepo.GetAll().Where(p => p.Name.Contains(freeText, ignoreCase) ||
            //                                                               p.DateOfBirth.Year.ToString().Contains(freeText, ignoreCase)
            //                                                               ).Select(p => new CastOrCrewSearchResultItem(p));

            var falseresult = leagueResult.Concat(teamResult);
            var Realresult = falseresult.Concat(playerResult);
            var result = Realresult.Concat(playerPersonalNumber);
            return result;
        }

        private LeagueRepository LeagueRepo => LeagueRepository.GetInstance();

        private TeamRepository TeamRepo => TeamRepository.GetInstance();

        private PersonRepository PersonRepo => PersonRepository.GetInstance();
    }
}
