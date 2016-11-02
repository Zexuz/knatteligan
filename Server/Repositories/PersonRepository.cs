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
            FilePath = GetFilePath("Perons.xml");
            _people = Load().ToList();
        }

        public static PersonRepository GetInstance()
        {
            return (PersonRepository)(Repo ?? (Repo = new PersonRepository()));
        }

        public void CreatePlayer(PersonName name, PersonalId dob)
        {
            var player = new Player(name, dob);
            AddAndSavePerson(player);
        }

        public void EditPlayer(Player player, PersonName name, PersonalId dob)
        {
            player.Name = name;
            player.PersonId = dob;
            AddAndSavePerson(player);
        }

        public void CreateCoach(PersonName name, PersonalId personalId, PhoneNumber phoneNumber, Email email)
        {
            var coach = new Coach(name, personalId, phoneNumber, email);
            AddAndSavePerson(coach);
        }

        public void EditCoach(Coach coach, PersonName name, PhoneNumber phoneNumber, Email email)
        {
            coach.Name = name;
            coach.PhoneNumber = phoneNumber;
            coach.Email = email;
            AddAndSavePerson(coach);
        }

        private void AddAndSavePerson(Person p)
        {
            _people.Add(p);
            Save(_people);
        }

        public override IEnumerable<Person> GetAll()
        {
            return _people;
        }
    }
}