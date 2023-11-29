
using Microsoft.EntityFrameworkCore;
using Data;

namespace CSDBapp
{
    public class CRUD<T> where T: class
    {
        private static CRUD<T>? _instance;

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
            using (GameDb _db = new GameDb())
            {
                return await _db.Set<T>().ToListAsync();
            }
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            using (GameDb _db = new GameDb())
            {
                return await _db.Set<T>().FindAsync(id);
            }
        }

        public async Task AddAsync(T entity)
        {
            using (GameDb _db = new GameDb())
            {
                _db.Set<T>().Add(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(T entity, params object[] key)
        {
            using (GameDb _db = new GameDb())
            {
                var existingEntity = await _db.Set<T>().FindAsync(key);

                if (existingEntity != null)
                {
                    _db.Entry(existingEntity).CurrentValues.SetValues(entity);
                    await _db.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (GameDb _db = new GameDb())
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
}
