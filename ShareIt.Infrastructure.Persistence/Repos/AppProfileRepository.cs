using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using ShareIt.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure
{
    public class AppProfileRepository : GenericRepository<AppProfile>, IAppProfileRepository
    {
        public readonly DefaultContext _Context;


        public AppProfileRepository(DefaultContext context, IMapper mapper) : base(context) 
        {
            _Context = context;
        }


        public async Task<AppProfile> UpdateAsync(AppProfile entity, string id)
        {


            try
            {
                var entry = await _Context.Set<AppProfile>().FindAsync(id);
                _Context.Entry(entry).CurrentValues.SetValues(entity);
                await _Context.SaveChangesAsync();

                return entry;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }




        }

        public async Task<AppProfile> GetByIdAsync(string id)
        {

            try
            {
                return await _Context.Set<AppProfile>().FindAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
