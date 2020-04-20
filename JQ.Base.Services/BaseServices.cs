using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JQ.Common.Helpers;
using JQ.Common.Infrastructure;
using JQ.Common.IRepository;
using JQ.Common.IServices;
using JQ.Common.Model;

namespace JQ.Base.Services
{
    public class BaseServices<T> : IBaseServices<T> where T : class, new()
    {

        public IBaseRepository<T> baseDal;

        #region 新增

        public async Task<bool> InsertAsync(T t, bool isIgnoreEx = true)
        {
            return await baseDal.InsertAsync(t, isIgnoreEx);
        }

        public async Task<bool> InsertBulkAsync(List<T> list, bool isIgnoreEx = true)
        {
            return await baseDal.InsertBulkAsync(list, isIgnoreEx);
        }

        #endregion

        #region 删除

        public async Task<bool> DeleteAsync(T t, bool isIgnoreEx = true)
        {
            return await baseDal.DeleteAsync(t, isIgnoreEx);
        }

        public async Task<bool> DeleteBulkAsync(List<T> list, bool isIgnoreEx = true)
        {
            return await baseDal.DeleteBulkAsync(list, isIgnoreEx);
        }

        public async Task<bool> DeleteBatchAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreEx = true)
        {
            return await baseDal.DeleteBatchAsync(whereExpression, isIgnoreEx);
        }


        #endregion

        #region 插入或更新
        public async Task<bool> InsertOrUpdateAsync(T t, bool isIgnoreEx = true)
        {
            return await baseDal.InsertOrUpdateAsync(t, isIgnoreEx);
        }

        public async Task<bool> InsertOrUpdateBulkAsync(List<T> list, bool isIgnoreEx = true)
        {
            return await baseDal.InsertOrUpdateBulkAsync(list, isIgnoreEx);
        }

        #endregion

        #region 更新

        public async Task<bool> UpdateAsync(T t, bool isIgnoreEx = true)
        {
            return await baseDal.UpdateAsync(t, isIgnoreEx);
        }

        public async Task<bool> UpdateBulkAsync(List<T> list, bool isIgnoreEx = true)
        {
            return await baseDal.UpdateBulkAsync(list, isIgnoreEx);

        }

        //public async Task<bool> UpdateBatchAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateColumn)
        //{
        //    return await baseDal.UpdateBatchAsync(whereExpression, updateColumn);
        //}

        //public async Task<bool> UpdateBatchAsync(Expression<Func<T, bool>> whereExpression, T t, List<string> updateColumns = null)
        //{
        //    return await baseDal.UpdateBatchAsync(whereExpression,t, updateColumns);
        //}


        #endregion

        #region 查询

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false)
        {
            return await baseDal.IsExistAsync(whereExpression, isIgnoreQueryFilters);
        }


        public async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false)
        {
            return await baseDal.CountAsync(whereExpression, isIgnoreQueryFilters);
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            return await baseDal.GetAsync(whereExpression, isIgnoreQueryFilters, isNoTracking);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> whereExpression, string orderBy, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            return await baseDal.GetAsync(whereExpression, orderBy, isIgnoreQueryFilters, isNoTracking);
        }
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            return await baseDal.GetListAsync(whereExpression, isIgnoreQueryFilters, isNoTracking);
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderExpression, bool isAsc = true, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            return await baseDal.GetListAsync(whereExpression, orderExpression, isAsc, isIgnoreQueryFilters, isNoTracking);
        }

        public List<T> GetList(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            return baseDal.GetList(whereExpression, isIgnoreQueryFilters, isNoTracking);
        }

        public List<T> GetList(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderExpression, bool isAsc = true, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            return baseDal.GetList(whereExpression, orderExpression, isAsc, isIgnoreQueryFilters, isNoTracking);
        }


        public async Task<PaginatedList<T>> GetPageAsync(PaginationBase paginationBase, IPropertyMapping propertyMapping, Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false)
        {
            return await baseDal.GetPageAsync(paginationBase, propertyMapping, whereExpression, isIgnoreQueryFilters);
        }
        public async Task<PaginatedList<T>> GetPageAsync(PaginationBase paginationBase, Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false)
        {
            return await baseDal.GetPageAsync(paginationBase, whereExpression, isIgnoreQueryFilters);
        }

        public async Task<IQueryable<T>> LoadAsync(Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            return await baseDal.LoadAsync(whereExpression, isIgnoreQueryFilters, isNoTracking);
        }

        #endregion




        #region 执行sql语句


        public async Task<int> ExecuteSqlAsync(string sql)
        {
            return await baseDal.ExecuteSqlAsync(sql);
        }

        public async Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList)
        {
            return await baseDal.ExecuteSqlAsync(sql, spList);
        }



        #endregion







    }
}
