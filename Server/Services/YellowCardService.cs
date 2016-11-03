using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class YellowCardService
    {
        private readonly YellowCardRepository _yellowCardRepository;

        public YellowCardService()
        {
            _yellowCardRepository = YellowCardRepository.GetInstance();
        }

        public IEnumerable<YellowCard> GetAll()
        {
            return _yellowCardRepository.GetAll();
        }

        public void Add(Guid playerGuid, Guid matchGuid)
        {
            _yellowCardRepository.Add(playerGuid, matchGuid);
        }



    }
}
