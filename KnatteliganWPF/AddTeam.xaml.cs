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
using knatteligan.Domain.ValueObjects;
using knatteligan.Services;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for AddTeam.xaml
    /// </summary>
    public partial class AddTeam : Window
    {
        public Team Team { get; set; }

        public TeamName TeamName1 { get; set; }

        public PersonName PersonName { get; set; }

        public PersonalNumber PersonalNumber { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public Email EmailAddress { get; set; }

        public List<Player> Players { get; set; }

        private readonly PersonService _personService;
        private readonly TeamService _teamService;
        public AddTeam()
        {
            InitializeComponent();
            _personService = new PersonService();
            _teamService = new TeamService();
            Players = new List<Player>();
            DataContext = this;

        }
        private void AddPlayer_Clicked(object sender, RoutedEventArgs e)
        {
            var addPlayer = new AddPlayer();
            addPlayer.ShowDialog();
  
        }
        private void CloseCommandHandler_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Team = new Team(TeamName1);
            _teamService.Add(Team);
            var coach = new Coach(PersonName, PersonalNumber, PhoneNumber, EmailAddress, Team);
            _personService.AddPerson(coach);

        }
    }
}
