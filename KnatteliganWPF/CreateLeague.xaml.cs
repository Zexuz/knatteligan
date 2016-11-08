using System.Collections.Generic;
using System.Windows;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Services;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for CreateLeague.xaml
    /// </summary>
    public partial class CreateLeague : Window
    {
        //Fuck wpf, really
        public League League { get; set; }
        public LeagueName LeagueName { get; set; }
        public List<Team> Teams { get; set; }

        private readonly LeagueService _leagueService;

        public CreateLeague()
        {
            InitializeComponent();
            _leagueService = new LeagueService();
            Teams = new List<Team>();
            DataContext = this;
        }

        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            var addTeam = new AddTeam();
            addTeam.ShowDialog();
        }
      
        private void AddLeague_Click(object sender, RoutedEventArgs e)
        {
            League = new League(LeagueName, Teams);

            _leagueService.AddLeague(League);
        }

        private void CloseCommandHandler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
