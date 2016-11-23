using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Repositories;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for PlayerStats.xaml
    /// </summary>
    public partial class PlayerStats : Window
    {
        private readonly bool _onlyOneTeam;

        public PlayerStats(Team team ):this(new List<Team>{team})
        {
            _onlyOneTeam = true;
        }

        public PlayerStats(List<Team> teams)
        {
            InitializeComponent();
            DataGrid.ItemsSource = SortingAlgoritm.Sort(teams, SortingAlgoritm.PlayerSortByTypes.Goal, true);
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Id" || e.PropertyName == "PersonalNumber")
                e.Cancel = true;

            if (_onlyOneTeam && e.PropertyName == "TeamName")
                e.Cancel = true;
        }
    }
}
