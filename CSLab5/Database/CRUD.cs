using Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CSDBapp
{
    class CRUD<T> where T: class
    {
        private static CRUD<T>? _instance;
        private GameDb _db;

        private CRUD()
        {
            _db = new GameDb();
        }

        public static CRUD<T> GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CRUD<T>();
            }
            return _instance;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _db.Set<T>().Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

    }
}
