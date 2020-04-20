using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JQ.Common.Infrastructure;
using JQ.Common.Model;


namespace JQ.Common.IRepository
{
    public interface IBaseRepository<T> where T : class
    {


        Task<bool> InsertAsync(T t, bool isIgnoreEx = true);
        Task<bool> InsertBulkAsync(List<T> list, bool isIgnoreEx = true);


        Task<bool> DeleteAsync(T t, bool isIgnoreEx = true);
        Task<bool> DeleteBulkAsync(List<T> list, bool isIgnoreEx = true);
        Task<bool> DeleteBatchAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreEx = true);


        Task<bool> UpdateAsync(T t, bool isIgnoreEx = true);
        Task<bool> UpdateBulkAsync(List<T> list, bool isIgnoreEx = true);
        // Task<bool> UpdateBatchAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateColumn);
        //  Task<bool> UpdateBatchAsync(Expression<Func<T, bool>> whereExpression, T t, List<string> updateColumns = null);


        Task<bool> InsertOrUpdateAsync(T t, bool isIgnoreEx = true);
        Task<bool> InsertOrUpdateBulkAsync(List<T> list, bool isIgnoreEx = true);


        Task<bool> IsExistAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false);
        Task<int> CountAsync(Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false);


        Task<T> GetAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>> whereExpression, string orderBy, bool isIgnoreQueryFilters = false, bool isNoTracking = true);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderExpression, bool isAsc = true, bool isIgnoreQueryFilters = false, bool isNoTracking = true);

        List<T> GetList(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true);
        List<T> GetList(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderExpression, bool isAsc = true, bool isIgnoreQueryFilters = false, bool isNoTracking = true);

        /// <summary>
        /// 一对多时候进行使用
        /// </summary>
        /// <param name="paginationBase"></param>
        /// <param name="propertyMapping"></param>
        /// <param name="whereExpression"></param>
        /// <param name="isIgnoreQueryFilters"></param>
        /// <returns></returns>
        Task<PaginatedList<T>> GetPageAsync(PaginationBase paginationBase, IPropertyMapping propertyMapping, Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false);
        /// <summary>
        /// 单表分页
        /// </summary>
        /// <param name="paginationBase"></param>
        /// <param name="whereExpression"></param>
        /// <param name="isIgnoreQueryFilters"></param>
        /// <returns></returns>
        Task<PaginatedList<T>> GetPageAsync(PaginationBase paginationBase, Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false);

        Task<IQueryable<T>> LoadAsync(Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false, bool isNoTracking = true);


        Task<int> ExecuteSqlAsync(string sql);
        Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList);

    }
}
