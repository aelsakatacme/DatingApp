using DatingApp.API.IRepositories;
using DatingApp.API.IServices;
using DatingApp.API.Models;
using DatingApp.API.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Services
{
    public class LookupService : ILookupService
    {
        IDatingRepository _repo;

        public LookupService(IDatingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Lookup>> GetLookup(string type)
        {
            IEnumerable<Lookup> res = Enumerable.Empty<Lookup>();
            switch (type.ToLower())
            {
                case "gender":
                    res = (await _repo.GetAll<Gender>()).Select(x => new Lookup { Id = x.Id, Value = x.Name });
                    break;
                case "city":
                    res = (await _repo.GetAll<City>()).Select(x => new Lookup { Id = x.Id, Value = x.Name });
                    break;
                case "country":
                    res = (await _repo.GetAll<Country>()).Select(x => new Lookup { Id = x.Id, Value = x.Name });
                    break;
            }
            return res;
        }
    }
}
