using System;
using knatteligan.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Repositories
{
    public class PersonRepository : Repository<Person>
    {

        private readonly List<Player> _players;
        private readonly List<Coach> _coaches;

        private static string _playerPath;
        private static string _coachPath;


        public PersonRepository()
        {
            _playerPath = GetFilePath("\\Persons\\Players.xml");
            _coachPath = GetFilePath("\\Persons\\Coaches.xml");

            _players = Load<Player>(_playerPath).ToList();
            _coaches = Load<Coach>(_coachPath).ToList();
        }

        #region GET

        public override IEnumerable<Person> GetAll()
        {
            var list = new List<Person>();
            list.AddRange(_coaches);
            list.AddRange(_players);

            return list;
        }

        public IEnumerable<Coach> GetAllCoaches()
        {
            return _coaches;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _players;
        }

        #endregion

        #region AddStuff


        public void Add(Person person)
        {
            var type = person.GetType();

            switch (type)
            {
                case Persons.Coach:
                    AddAndSaveCoach(person);
                    break;
                case Persons.Player:
                    AddAndSavePlayer(person);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddAndSaveCoach(Person person)
        {
            var coach = (Coach)person;
            _coaches.Add(coach);
            Save(_coachPath, _coaches);

        }

        private void AddAndSavePlayer(Person person)
        {
            var player = (Player)person;
            _players.Add(player);
            Save(_playerPath, _players);
        }

        #endregion

        //TODO: Just one search method for both coach and player.

        public Player FindPlayerById(Guid id)
        {
            return _players.Find(x => x.Id == id);
        }

        public Coach FindCoachById(Guid id)
        {
            return _coaches.Find(x => x.Id == id);
        }

        public static PersonRepository GetInstance()
        {
            return (PersonRepository)(Repo ?? (Repo = new PersonRepository()));
        }

        public Person FindBy(Guid personId)
        {
            return GetAll().First(cp => cp.Id == personId);
        }


        public void RemovePlayer(Guid id)
        {
            var player = FindPlayerById(id);

            _players.Remove(player);

            Save(_coachPath, _coaches);
        }

        public void Save()
        {
            Save(_playerPath, _players);
            Save(_coachPath, _coaches);
        }


        public void Edit(Player player, PersonName name, PersonalNumber personId)
        {
            player.Name = name;
            player.PersonalNumber = personId;
            Save(_playerPath, _players);
        }
    }
}