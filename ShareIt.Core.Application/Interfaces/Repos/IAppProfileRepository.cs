using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public interface IAppProfileRepository : IGenericRepository<AppProfile>
    {
        Task<AppProfile> UpdateAsync(AppProfile entity, string id);
        Task<AppProfile> GetByIdAsync(string id);
    }
}
