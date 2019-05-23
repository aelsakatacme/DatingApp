using DatingApp.API.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.IServices
{
    public interface ILookupService
    {
        Task<IEnumerable<Lookup>> GetLookup(string type);
    }
}
