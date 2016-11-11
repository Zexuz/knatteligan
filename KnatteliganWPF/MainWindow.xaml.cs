using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;
using knatteligan.Services;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LeagueService leagueService;

        public MainWindow()
        {
            InitializeComponent();
           // leagueService = new LeagueService();
          //  leagueList.ItemsSource = leagueService.GetAllLeagues();
            // var x = PersonRepository.GetInstance().GetAll();
        }
        private void CreateLeague_Clicked(object sender, RoutedEventArgs e)
        {
            var createLeague = new CreateLeague();
            var createLeagueResult = createLeague.ShowDialog();
        }

        private void ManageLeague_Clicked(object sender, RoutedEventArgs e)
        {
            var manageLeague = new ManageLeague();
            var manageLeagueResult = manageLeague.ShowDialog();
           

        }
        private void Matchprotocoll_Clicked(object sender, RoutedEventArgs e)
        {
            var matchProtocol = new MatchProtocol();
            var matchProtocolResult = matchProtocol.ShowDialog();
        }
    }
}