using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace knatteligan.Repositories
{

    public class PersonRepository : Repository<Person>
    {

        private readonly List<Person> _people;
        protected override string FilePath { get; }

        public PersonRepository()
        {
            FilePath = GetFilePath("Persons.xml");
            _people = Load().ToList();
        }

        public static PersonRepository GetInstance()
        {
            return (PersonRepository)(Repo ?? (Repo = new PersonRepository()));
        }

        public void Add(PersonName name, PersonalNumber dob, Team team)
        {
            var player = new Player(name, dob, team);
            AddAndSavePerson(player);
        }

        public void Add(PersonName name, PersonalNumber personalNumber, PhoneNumber phoneNumber, Email email, Team team)
        {
            var coach = new Coach(name, personalNumber, phoneNumber, email, team);
            AddAndSavePerson(coach);
        }

        public void Edit(Player player, PersonName name, PersonalNumber dob)
        {
            player.Name = name;
            player.PersonalNumber = dob;
            AddAndSavePerson(player);
        }

        public void Edit(Coach coach, PersonName name, PhoneNumber phoneNumber, Email email)
        {
            coach.Name = name;
            coach.PhoneNumber = phoneNumber;
            coach.Email = email;
            AddAndSavePerson(coach);
        }

        private void AddAndSavePerson(Person person)
        {
            _people.Add(person);
            Save(_people);
        }


        //TODO: Save _teams too.
        public void AddPlayerToTeam(Team team, Player player)
        {
            team.TeamPersons.Add(player.Id);
            _people.Add(player);
            Save(_people);
        }

        public void RemovePlayerFromTeam(Team team, Player player)
        {
            team.TeamPersons.Remove(player.Id);
            _people.Add(player);
            Save(_people);
        }

        public override IEnumerable<Person> GetAll()
        {
            return _people;
        }
    }
}