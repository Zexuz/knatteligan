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
        public TeamName TeamName { get; set; }
        public List<TeamPerson> TeamPersons { get; set; }
        public PersonName PersonName { get; set; }
        public PersonalNumber PersonalNumber { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email EmailAddress { get; set; }
        public List<Player> Players { get; set; }
        

        public AddTeamWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AddPlayer_Clicked(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow();
            addPlayerWindow.ShowDialog();
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
    }
}
