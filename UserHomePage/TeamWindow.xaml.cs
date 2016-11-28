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
using knatteligan.Services;
using KnatteliganWPF;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;
        private Guid _teamId;
        public TeamWindow(Guid teamId)

        {
            InitializeComponent();
            _teamId = teamId;
            _matchService = new MatchService();
            _teamService = new TeamService();
            var teamMatchList = _matchService.GetAll().Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId).OrderBy(m => m.MatchDate);
            TeamMatchList.ItemsSource = teamMatchList;

        }
        
        private void TeamMatchList_OnClick(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void TeamMatchList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListBox;
            var match = (Match)listItem.SelectedItems[0];
            var matchProtocol = new MatchProtocol(match);
            matchProtocol.Show();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var team = _teamService.FindById(_teamId);
            PlayerStats playerStats = new PlayerStats(team);
            playerStats.ShowDialog();
        }

        private void ButtonBase_OnClick_Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
