using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DatingApp.API.IRepositories
{
    public interface IDatingRepository
    {
        Task<IEnumerable<T>> GetAll<T>(params Expression<Func<T, object>>[] includes) where T : class;
        Task<T> Get<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;
        void Add<T>(ref T entity) where T : class;
        void Update<T>(ref T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        Task<bool> SaveChanges();


        #region User
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int UserId);
        #endregion

    }
}
