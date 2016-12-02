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

        public PlayerStatsPage(Team team) : this(new List<Team> {team})
        {
            DataGrid.Columns[1].Visibility = Visibility.Collapsed;
        }

        public PlayerStatsPage(List<Team> teams)
        {
            DataContext = this;
            InitializeComponent();

            DataGrid.AutoGenerateColumns = false;
            DataGrid.ItemsSource = SortingAlgorithm.Sort(teams, SortingAlgorithm.PlayerSortByTypes.Goal, true);
        }

    }
}
