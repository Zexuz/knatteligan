using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Services;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for AddTeamWindow.xaml
    /// </summary>
    public partial class AddTeamWindow : Window
    {
        public Team Team { get; set; }
        public Coach Coach { get; set; }
        public TeamName TeamName { get; set; }
        public List<TeamPerson> TeamPersons { get; set; }
        public PersonName PersonName { get; set; }
        public PersonalNumber PersonalNumber { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email EmailAddress { get; set; }
        public List<Player> Players { get; set; }

        private readonly PersonService _personService;



        public AddTeamWindow()
        {
            InitializeComponent();
            PlayerList.ItemsSource = Players;
            _personService = new PersonService();
            DataContext = this;
        }

        private void AddPlayer_Clicked(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow();
            var addPlayerResult = addPlayerWindow.ShowDialog();
            if (!addPlayerResult.HasValue) return;

            if (Players.Count >= 20)
            {
                AddTeamBtn.IsEnabled = true;
            }

            _personService.Add(addPlayerWindow.Player);
            Players = _personService.GetAllPlayers().ToList();
            //Teams.Add(addTeamWindow.Team);
            PlayerList.ItemsSource = new ObservableCollection<Player>(Players);
        }

        private void CloseCommandHandler_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            Team = new Team(TeamName, TeamPersons);

            DialogResult = true;
            Close();
        }

        private void RemoveTeam_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditTeam_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveTeamBtn.IsEnabled = true;
            EditTeamBtn.IsEnabled = true;
        }
    }
}
