using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.IRepositories;
using DatingApp.API.Models;
using DatingApp.API.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatingApp.API.Repositories
{
    public class DatingRepository : IDatingRepository
    {
        DatingAppContext _context;

        public DatingRepository(DatingAppContext context)
        {
            _context = context;
        }


        private IQueryable<T> InsializeQuery<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public async Task<IEnumerable<T>> GetAll<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            return await InsializeQuery(includes).ToListAsync();
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            return await InsializeQuery(includes).FirstOrDefaultAsync(predicate);
        }

        public void Add<T>(ref T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(ref T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            try { return await _context.SaveChangesAsync() > 0; }
            catch (Exception ex) { return false; }
        }


        #region User
        public async Task<User> GetUser(int UserId)
        {
            return await _context.Users
                .Include(x => x.Country)
                .Include(x => x.City)
                .Include(x => x.Gender)
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == UserId);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users
                .Include(x => x.Country)
                .Include(x => x.City)
                .Include(x => x.Gender)
                .Include(x => x.Photos)
                .ToListAsync();
        }
        #endregion

    }
}
