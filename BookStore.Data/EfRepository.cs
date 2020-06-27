using BookStore.Data.Models.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BookStoreDatabase _context;
        private DbSet<T> _entities;

        public EfRepository(BookStoreDatabase context)
        {
            this._context = context;
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Add(entity);
            this._context.SaveChanges();
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            await this.Entities.AddAsync(entity);
            await this._context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this._context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            await this._context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Remove(entity);
            this._context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public T GetById(object id, bool noTracking = false)
        {
            if (noTracking)
                return this.TableNoTracking.FirstOrDefault(x => x.Id.Equals(id));

            return this.Entities.Find(id);
        }

        public async Task<T> GetByIdAsync(object id, bool noTracking = false)
        {
            if (noTracking)
                return await this.TableNoTracking.FirstOrDefaultAsync(x => x.Id.Equals(id));

            return await this.Entities.FindAsync(id);
        }

        public IQueryable<T> Table => this.Entities;

        public IQueryable<T> TableNoTracking => this.Entities.AsNoTracking();

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();

                return _entities;
            }
        }
    }
}
