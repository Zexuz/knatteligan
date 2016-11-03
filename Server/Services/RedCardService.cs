using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class RedCardService
    {
        private readonly RedCardRepository _redCardRepository;

        public RedCardService()
        {
            _redCardRepository = RedCardRepository.GetInstance();
        }

        public IEnumerable<RedCard> GetAll()
        {
            return _redCardRepository.GetAll();
        }

        public void Add(Guid playerGuid, Guid matchGuid)
        {
            _redCardRepository.Add(playerGuid, matchGuid);
        }



    }
}
