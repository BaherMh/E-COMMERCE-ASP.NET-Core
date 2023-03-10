using AppDbContext.IRepos;
using AppDbContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDbContext.Repos
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected ECOMMERCEContext _db;

        private DbSet<T> _dbSet;
        public BaseRepo(ECOMMERCEContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public T Get(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }
    }
}
