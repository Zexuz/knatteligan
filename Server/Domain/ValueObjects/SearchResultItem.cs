using knatteligan.Domain.Entities;

namespace knatteligan
{
    public enum SearchResultItemType
    {
        None, League, Player, Team
    }
    public class SearchResultItem
    {
        public SearchResultItem(object item)
        {
            ResultItem = item;
        }

        public object ResultItem { get; }
        public virtual SearchResultItemType Type { get { return SearchResultItemType.None; } }
    }

    public class LeagueSearchResultItem : SearchResultItem
    {
        public LeagueSearchResultItem(League league)
            : base(league)
        {

        }

        public override SearchResultItemType Type => SearchResultItemType.League;

        public override string ToString()
        {
            var league = (League)ResultItem;
            return $"{league.Name}";
        }

        //public override string ToString()
        //{
        //    return $"[Movie]: {((Movie)ResultItem).Title} ({((Movie)ResultItem).ProductionYear.Value})";
        //}
    }

    public class PlayerSearchResultItem : SearchResultItem
    {
        public PlayerSearchResultItem(Player player)
            : base(player)
        {
        }
        public override string ToString()
        {
            var player = (Player)ResultItem;
            return $"{player.Name}";
        }


        public override SearchResultItemType Type => SearchResultItemType.League;


    }
    public class TeamSearchResultItem : SearchResultItem
    {
        public TeamSearchResultItem(Team team)
            : base(team)
        {
        }

        public override SearchResultItemType Type => SearchResultItemType.Team;

        public override string ToString()
        {
            var team = (Team)ResultItem;
            return $"{team.Name.Value}";
        }
    }
}

//public override string ToString()
//{
//    var castOrCrew = (CastOrCrew)ResultItem;
//    return $"[{castOrCrew.JobTitle}]: {castOrCrew.Name} ({castOrCrew.DateOfBirth:yyy-MM-dd})";
//}

