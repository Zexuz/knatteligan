using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        private int _counter;
        private Dictionary<SortingAlgoritm.PlayerSortByTypes, bool> _lastState;

        public PlayerStats(Team team) : this(new List<Team> {team})
        {
            _onlyOneTeam = true;
            DataGrid.Columns[5].Visibility = Visibility.Collapsed;
        }

        public PlayerStats(List<Team> teams)
        {
            DataContext = this;
            InitializeComponent();

            DataGrid.AutoGenerateColumns = false;
            DataGrid.ItemsSource = SortingAlgoritm.Sort(teams, SortingAlgoritm.PlayerSortByTypes.Goal, true);
            DataGrid.Sorting += CustomSortHandeler;

            _lastState = new Dictionary<SortingAlgoritm.PlayerSortByTypes, bool>
            {
                {SortingAlgoritm.PlayerSortByTypes.Assist, false},
                {SortingAlgoritm.PlayerSortByTypes.Goal, false},
                {SortingAlgoritm.PlayerSortByTypes.PlayerName, false},
                {SortingAlgoritm.PlayerSortByTypes.Redcard, false},
                {SortingAlgoritm.PlayerSortByTypes.TeamName, false},
                {SortingAlgoritm.PlayerSortByTypes.Yellowcard, false}
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
                case "RedCardIds":
                    e.Column.Header = "RC";
                    break;
                case "YellowCardIds":
                    e.Column.Header = "YC";
                    break;
            }

            if (_onlyOneTeam && e.PropertyName == "TeamName")
                e.Cancel = true;
        }


        private void CustomSortHandeler(object sender, DataGridSortingEventArgs e)
        {
            var column = e.Column;

            if(column.SortDirection == null) return;


            SortingAlgoritm.PlayerSortByTypes type;
            switch (column.Header.ToString())
            {
                case "G":
                    type = SortingAlgoritm.PlayerSortByTypes.Goal;
                    break;
                case "A":
                    type = SortingAlgoritm.PlayerSortByTypes.Assist;
                    break;
                case "RC":
                    type = SortingAlgoritm.PlayerSortByTypes.Redcard;
                    break;
                case "YC":
                    type = SortingAlgoritm.PlayerSortByTypes.Yellowcard;
                    break;
                case "Name":
                    type = SortingAlgoritm.PlayerSortByTypes.PlayerName;
                    break;
                case "Team":
                    type = SortingAlgoritm.PlayerSortByTypes.TeamName;
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

            var listOfPlayer = (List<SortingAlgoritm.PlayerStatsInfoItem>) DataGrid.Items.SourceCollection;

            var list = SortingAlgoritm.Sort(listOfPlayer, type,_lastState[type]);
            Trace.WriteLine(list[0].GoalIds.Count);
            DataGrid.ItemsSource = list;
        }
    }
}