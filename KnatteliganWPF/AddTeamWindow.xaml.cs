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
    /// Interaction logic for AddTeamWindow.xaml
    /// </summary>
    public partial class AddTeamWindow : Window
    {
        public Team Team { get; set; }
        public Coach Coach { get; set; }
        public List<Player> Players { get; set; }
        public TeamName TeamName { get; set; }
        public PersonName PersonName { get; set; }
        public PersonalNumber PersonalNumber { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email EmailAddress { get; set; }


        public AddTeamWindow()
        {
            InitializeComponent();
            PlayerList.ItemsSource = Players;
            DataContext = this;
        }

        private void AddPlayer_Clicked(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayer();
            var addPlayerResult = addPlayerWindow.ShowDialog();
            if (!addPlayerResult.HasValue) return;

            if (Players.Count >= 20)
            {
                AddTeamBtn.IsEnabled = true;
            }
        }

        private void CloseCommandHandler_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            Team = new Team(TeamName, Players, Coach);

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
