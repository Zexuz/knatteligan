using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for PlayerStatsPage.xaml
    /// </summary>
    public partial class PlayerStatsPage : Page
    {
        private readonly bool _onlyOneTeam;
        private readonly Dictionary<SortingAlgorithm.PlayerSortByTypes, bool> _lastState;
        //private int _counter;

        public PlayerStatsPage(Team team) : this(new List<Team> {team})
        {
            _onlyOneTeam = true;
            //Removes team column
            DataGrid.Columns[1].Visibility = Visibility.Collapsed;
        }

        public PlayerStatsPage(List<Team> teams)
        {
            DataContext = this;
            InitializeComponent();

            DataGrid.AutoGenerateColumns = false;
            DataGrid.ItemsSource = SortingAlgorithm.Sort(teams, SortingAlgorithm.PlayerSortByTypes.Goal, true);
            DataGrid.Sorting += CustomSortHandeler;

            _lastState = new Dictionary<SortingAlgorithm.PlayerSortByTypes, bool>
            {
                {SortingAlgorithm.PlayerSortByTypes.Assist, false},
                {SortingAlgorithm.PlayerSortByTypes.Goal, false},
                {SortingAlgorithm.PlayerSortByTypes.PlayerName, false},
                {SortingAlgorithm.PlayerSortByTypes.Redcard, false},
                {SortingAlgorithm.PlayerSortByTypes.TeamName, false},
                {SortingAlgorithm.PlayerSortByTypes.Yellowcard, false}
            };
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Id":
                case "PersonalNumber":
                    e.Cancel = true;
                    break;
                case "GoalIds":
                    e.Column.Header = "G";
                    break;
                case "AssistCount":
                    e.Column.Header = "A";
                    break;
                case "YellowCardIds":
                    e.Column.Header = "YC";
                    break;
                case "RedCardIds":
                    e.Column.Header = "RC";
                    break;
            }

            if (_onlyOneTeam && e.PropertyName == "TeamName")
                e.Cancel = true;
        }

        private void CustomSortHandeler(object sender, DataGridSortingEventArgs e)
        {
            var column = e.Column;
            if (column.SortDirection == null) return;

            SortingAlgorithm.PlayerSortByTypes type;
            switch (column.Header.ToString())
            {
                case "Name":
                    type = SortingAlgorithm.PlayerSortByTypes.PlayerName;
                    break;
                case "Team":
                    type = SortingAlgorithm.PlayerSortByTypes.TeamName;
                    break;
                case "G":
                    type = SortingAlgorithm.PlayerSortByTypes.Goal;
                    break;
                case "A":
                    type = SortingAlgorithm.PlayerSortByTypes.Assist;
                    break;
                case "YC":
                    type = SortingAlgorithm.PlayerSortByTypes.Yellowcard;
                    break;
                case "RC":
                    type = SortingAlgorithm.PlayerSortByTypes.Redcard;
                    break;
                default:
                    return;
            }
            _lastState[type] = !_lastState[type];

            Trace.WriteLine(_lastState[type]);

            if (_lastState[type])
            {
                column.SortDirection = ListSortDirection.Descending;
            }
            else
            {
                column.SortDirection = ListSortDirection.Ascending;
            }

            var listOfPlayer = (List<SortingAlgorithm.PlayerStatsInfoItem>)DataGrid.Items.SourceCollection;

            var list = SortingAlgorithm.Sort(listOfPlayer, type, _lastState[type]);
            Trace.WriteLine(list[0].GoalIds.Count);
            DataGrid.ItemsSource = list;
        }

    }
}
