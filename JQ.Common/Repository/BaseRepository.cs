using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JQ.Common.IRepository;
using JQ.Common.Model;
using JQ.Common.Infrastructure;
using System.Reflection;
using EFCore.BulkExtensions;

namespace JQ.Common.Repository
{

    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {

        public ApplicationDbContext _context;
        public DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        async Task<int> SaveChangesAsync(bool isIgnoreEx = false)
        {
            int i = 0;

            if (isIgnoreEx)
            {
                try
                {
                    i = await _context.SaveChangesAsync();
                }
                // catch (System.Data.t.Validation.DbtValidationException dbEx)//捕获实体验证异常
                //{
                //    var sb = new StringBuilder();
                //    dbEx.tValidationErrors.First().ValidationErrors.ToList().ForEach(i =>
                //    {
                //        sb.AppendFormat("属性为：{0}，信息为：{1}\n\r", i.PropertyName, i.ErrorMessage);
                //    });
                //    if (Logger == null)
                //        throw new Exception(sb.ToString());
                //    Logger(sb.ToString() + "处理时间：" + DateTime.Now);

                //}
                //catch (OptimisticConcurrencyException)//并发冲突异常
                //{

                //}
                catch //捕获所有异常
                {

                }

            }
            else
            {
                i = await _context.SaveChangesAsync();
            }
            return i;
        }

        #region 新增

        public async Task<bool> InsertAsync(T t, bool isIgnoreEx = true)
        {
            _dbSet.Add(t);
            return await SaveChangesAsync(isIgnoreEx) > 0;
        }

        public async Task<bool> InsertBulkAsync(List<T> list, bool isIgnoreEx = true)
        {
            if (_context.Database.IsSqlServer())
            {
                if (isIgnoreEx)
                {
                    try
                    {
                        await _context.BulkInsertAsync(list);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    await _context.BulkInsertAsync(list);
                    return true;
                }

            }
            else
            {
                _dbSet.AddRange(list);
                return await SaveChangesAsync(isIgnoreEx) > 0;
            }

        }

        #endregion

        #region 删除

        public async Task<bool> DeleteAsync(T t, bool isIgnoreEx = true)
        {
            _dbSet.Remove(t);
            return await SaveChangesAsync(isIgnoreEx) > 0;
        }

        public async Task<bool> DeleteBulkAsync(List<T> list, bool isIgnoreEx = true)
        {
            list.ForEach(t =>
            {
                _dbSet.Remove(t);
            });
            return await SaveChangesAsync(isIgnoreEx) > 0;
        }

        public async Task<bool> DeleteBatchAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreEx = true)
        {
            if (_context.Database.IsSqlServer())
            {
                if (isIgnoreEx)
                {
                    try
                    {
                        return await _dbSet.Where(whereExpression).BatchDeleteAsync() > 0;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return await _dbSet.Where(whereExpression).BatchDeleteAsync() > 0;
                }

            }
            else
            {
                var list = _dbSet.Where(whereExpression).ToList();
                list.ForEach(t =>
                {
                    _dbSet.Remove(t);
                });
                return await SaveChangesAsync(isIgnoreEx) > 0;
            }
        }


        #endregion

        #region 插入或更新
        public async Task<bool> InsertOrUpdateAsync(T t, bool isIgnoreEx = true)
        {
            if (t == null)
            {
                return false;
            }

            if (_context.Database.IsSqlServer())
            {
                try
                {
                    await _context.BulkInsertOrUpdateAsync(new List<T>() { t });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                _dbSet.Update(t);
                return await SaveChangesAsync(isIgnoreEx) > 0;
            }



        }

        public async Task<bool> InsertOrUpdateBulkAsync(List<T> list, bool isIgnoreEx = true)
        {
            if (list == null)
            {
                return false;
            }

            if (_context.Database.IsSqlServer())
            {
                try
                {
                    await _context.BulkInsertOrUpdateAsync(list);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                list.ForEach(c =>
                {
                    _dbSet.Update(c);
                });

                return await SaveChangesAsync(isIgnoreEx) > 0;
            }


        }

        #endregion

        #region 更新

        public async Task<bool> UpdateAsync(T t, bool isIgnoreEx = true)
        {
            _dbSet.Update(t);
            return await SaveChangesAsync(isIgnoreEx) > 0;
        }

        public async Task<bool> UpdateBulkAsync(List<T> list, bool isIgnoreEx = true)
        {
            if (list == null || list.Count == 0)
            {
                return false;
            }

            if (_context.Database.IsSqlServer())
            {
                try
                {
                    await _context.BulkUpdateAsync(list);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                list.ForEach(c =>
                {
                    _dbSet.Update(c);
                });
                return await SaveChangesAsync(isIgnoreEx) > 0;
            }

        }

        public async Task<bool> UpdateBatchAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateColumn)
        {
            if (_context.Database.IsSqlServer())
            {
                return await _dbSet.Where(whereExpression).BatchUpdateAsync(updateColumn) > 0;
            }
            else
            {
                throw new Exception("此方法只支持sqlserver2008+和sqlite");
            }
        }

        public async Task<bool> UpdateBatchAsync(Expression<Func<T, bool>> whereExpression, T t, List<string> updateColumns = null)
        {

            if (_context.Database.IsSqlServer())
            {
                return await _dbSet.Where(whereExpression).BatchUpdateAsync(t, updateColumns) > 0;
            }
            else
            {
                throw new Exception("此方法只支持sqlserver2008+和sqlite");
            }

        }


        #endregion

        #region 查询

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false)
        {
            var iQueryable = isIgnoreQueryFilters ? _dbSet.IgnoreQueryFilters() : _dbSet.AsQueryable();
            return await iQueryable.AnyAsync(whereExpression);
        }


        public async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false)
        {
            if (whereExpression == null)
            {
                whereExpression = e => true;
            }

            var iQueryable = isIgnoreQueryFilters ? _dbSet.IgnoreQueryFilters() : _dbSet.AsQueryable();

            return await iQueryable.CountAsync(whereExpression);
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            var iQueryable = isNoTracking ? _dbSet.Where(whereExpression).AsNoTracking() : _dbSet.Where(whereExpression);
            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;

            return await iQueryable.FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> whereExpression, string orderBy, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            if (whereExpression == null)
            {
                whereExpression = e => true;
            }
            var iQueryable = isNoTracking ? _dbSet.Where(whereExpression).AsNoTracking() : _dbSet.Where(whereExpression);
            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;
            iQueryable = iQueryable.OrderByBatch(orderBy);

            return await iQueryable.FirstOrDefaultAsync();
        }



        IQueryable<T> GetListQueryable(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            if (whereExpression == null)
            {
                whereExpression = e => true;
            }
            var iQueryable = isNoTracking ? _dbSet.Where(whereExpression).AsNoTracking() : _dbSet.Where(whereExpression);
            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;
            return iQueryable;
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            var query = GetListQueryable(whereExpression, isIgnoreQueryFilters, isNoTracking);
            return await query.ToListAsync();
        }

        public List<T> GetList(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            var query = GetListQueryable(whereExpression, isIgnoreQueryFilters, isNoTracking);
            return query.ToList();
        }

        IQueryable<T> GetListQueryable(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderExpression, bool isAsc = true, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            if (whereExpression == null)
            {
                whereExpression = e => true;
            }
            var iQueryable = isNoTracking ? _dbSet.Where(whereExpression).AsNoTracking() : _dbSet.Where(whereExpression);
            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;
            if (orderExpression != null)
            {
                iQueryable = isAsc ? iQueryable.OrderBy(orderExpression) : iQueryable.OrderByDescending(orderExpression);
            }
            return iQueryable;
        }
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderExpression, bool isAsc = true, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            var query = GetListQueryable(whereExpression, orderExpression, isAsc, isIgnoreQueryFilters, isNoTracking);
            return await query.ToListAsync();
        }

        public List<T> GetList(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderExpression, bool isAsc = true, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            var query = GetListQueryable(whereExpression, orderExpression, isAsc, isIgnoreQueryFilters, isNoTracking);
            return query.ToList();
        }


        public async Task<PaginatedList<T>> GetPageAsync(PaginationBase paginationBase, IPropertyMapping propertyMapping, Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false)
        {
            if (whereExpression == null)
            {
                whereExpression = e => true;
            }
            if (paginationBase.PageIndex == 0)
            {
                whereExpression = e => false;
            }
            var iQueryable = _dbSet.Where(whereExpression);

            var count = (await iQueryable.ToListAsync()).Count;

            int index = paginationBase.PageIndex - 1;
            int size = paginationBase.PageSize;

            string orderBy = string.IsNullOrEmpty(paginationBase.OrderBy) ? "ID" : paginationBase.OrderBy;

            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;
            iQueryable = iQueryable.ApplySort(paginationBase.OrderBy, propertyMapping);

            var data = await iQueryable.Skip(index * size).Take(size).ToListAsync();

            var pageList = new PaginatedList<T>(paginationBase.PageIndex, paginationBase.PageSize, count, data);

            return await Task.Run(() => pageList);

        }


        public async Task<PaginatedList<T>> GetPageAsync(PaginationBase paginationBase, Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false)
        {
            if (whereExpression == null)
            {
                whereExpression = e => true;
            }
            if (paginationBase.PageIndex == 0)
            {
                whereExpression = e => false;
            }
            int index = paginationBase.PageIndex - 1;
            int size = paginationBase.PageSize;
            string orderBy = string.IsNullOrEmpty(paginationBase.OrderBy) ? "ID" : paginationBase.OrderBy;


            var iQueryable = _dbSet.Where(whereExpression);
            
            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;

            var count = (await iQueryable.ToListAsync()).Count;
            var data = await iQueryable.OrderBy(orderBy).Skip(index * size).Take(size).ToListAsync();

            var pageList = new PaginatedList<T>(paginationBase.PageIndex, paginationBase.PageSize, count, data);

            return await Task.Run(() => pageList);
        }



        public async Task<IQueryable<T>> LoadAsync(Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false, bool isNoTracking = true)
        {
            if (whereExpression == null)
            {
                whereExpression = c => true;
            }
            var iQueryable = isNoTracking ? _dbSet.Where(whereExpression).AsNoTracking() : _dbSet.Where(whereExpression);
            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;

            return await Task.Run(() => iQueryable);
        }

        #endregion




        #region 执行sql语句


        public async Task<int> ExecuteSqlAsync(string sql)
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql);
        }

        public async Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList)
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql, spList.ToArray());
        }

        #endregion

    }
}
