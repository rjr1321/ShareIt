using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.EntityFrameworkCore;
using ShareIt.Core.Application;
using ShareIt.Infrastructure.Persistence.Context;

namespace ShareIt.Infrastructure.Persistence.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        readonly DefaultContext _context;
    

        public GenericRepository(DefaultContext context)
        {
            _context = context;
     
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;



            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
           
        }


        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                var entry = await GetByIdAsync(id);
                _context.Set<T>().Remove(entry);
                await _context.SaveChangesAsync();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
               
            }

        }




        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            try
            {
                var query = _context.Set<T>().AsQueryable();

              
                var navigationProperties = _context.Model.FindEntityType(typeof(T)).GetNavigations();
                foreach (var property in navigationProperties)
                {
                    query = query.Include(property.Name);
                }

                return await query.ToListAsync();
        
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }




        public virtual async Task<ICollection<T>> GetAllWithIncludeAsync(List<string> properties)
        {

            try
            {
                var query = _context.Set<T>().AsQueryable();

                foreach (string property in properties)
                {
                    query = query.Include(property);
                }

                return await query.ToListAsync();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {

            try
            {
               return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public virtual async Task<T> UpdateAsync(T entity, int id)
        {


            try
            {
                var entry = await _context.Set<T>().FindAsync(id);
                _context.Entry(entry).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return entry;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }



          
        }


    }




}
