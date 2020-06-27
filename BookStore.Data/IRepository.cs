using BookStore.Data.Models.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public partial interface IRepository<T> where T : BaseEntity
    {
        void Insert(T entity);

        Task InsertAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        T GetById(object id, bool noTracking = false);

        Task<T> GetByIdAsync(object id, bool noTracking = false);

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }
    }
}
