using System;
using Quality.Common;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quality.DataAccess
{
    public interface IGenericBase<T> where T : class
    {
        string ConnectionString { get; }

        Task<IEnumerable<T>> All();
        Task<T> ById(int id);
        Task<Result> Delete(T data);
        Task<Result> SoftDeleteAsync(int id);
        Task<Result> Insert(T data);
        Task<Result> Update(T data);
        Result BulkInsert(List<T> data);
        Task<List<T>> GetAllFilteredBy(Expression<Func<T, bool>> condition);
        Task<T> GetSingleFilteredByAsync(Expression<Func<T, bool>> condition);
        Task<Result> InsertBulkAsync(List<T> entities);
    }
}