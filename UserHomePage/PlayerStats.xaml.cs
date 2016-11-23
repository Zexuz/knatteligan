using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for PlayerStats.xaml
    /// </summary>
    public partial class PlayerStats : Window
    {
        private readonly bool _onlyOneTeam;

        public PlayerStats(Team team) : this(new List<Team> {team})
        {
            _onlyOneTeam = true;
        }

        public PlayerStats(List<Team> teams)
        {
            InitializeComponent();
            DataGrid.ItemsSource = SortingAlgoritm.Sort(teams, SortingAlgoritm.PlayerSortByTypes.Goal, true);
            DataGrid.Sorting += CustomSortHandeler;
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Id":
                case "PersonalNumber":
                    e.Cancel = true;
                    break;
                case "GoalCount":
                    e.Column.Header = "G";
                    break;
                case "AssistCount":
                    e.Column.Header = "A";
                    break;
                case "RedCardCount":
                    e.Column.Header = "RC";
                    break;
                case "YellowCardCount":
                    e.Column.Header = "YC";
                    break;
            }

            if (_onlyOneTeam && e.PropertyName == "TeamName")
                e.Cancel = true;
        }


        private void CustomSortHandeler(object sender, DataGridSortingEventArgs e)
        {
//            var colum = e.Column;
//
//            var desc = colum.SortDirection.GetValueOrDefault() == ListSortDirection.Descending;
//
//            var listOfPlayer = (List<SortingAlgoritm.PlayerStatsInfoItem>)DataGrid.Items.SourceCollection;
//            DataGrid.ItemsSource = SortingAlgoritm.Sort(listOfPlayer, SortingAlgoritm.PlayerSortByTypes.PlayerName, desc);
//
//            Trace.WriteLine(colum.Header.ToString());
        }
    }
}