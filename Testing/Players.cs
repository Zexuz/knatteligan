﻿using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Services;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class Players
    {
        private List<Player> _players;
        private PersonService _personServive;

        [OneTimeSetUp]
        public void Init()
        {
            _players = new List<Player>();
            _personServive = new PersonService();
        }
    }
}