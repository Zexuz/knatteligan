using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class AssistService
    {
        private readonly AssistRepository _assistRepository;

        public AssistService()
        {
            _assistRepository = AssistRepository.GetInstance();
        }

        public IEnumerable<Assist> GetAll()
        {
            return _assistRepository.GetAll();
        }

        public void Add(Guid playerGuid, Guid matchGuid)
        {
            _assistRepository.Add(playerGuid, matchGuid);
        }



    }
}
