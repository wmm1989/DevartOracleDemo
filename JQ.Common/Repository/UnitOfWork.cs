using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JQ.Common.Helpers;
using JQ.Common.Infrastructure;
using JQ.Common.IRepository;
using JQ.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Oracle.ManagedDataAccess.Client;

namespace JQ.Common.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public DbContext GetDbContext()
        {
            return _context;
        }

        public DbTransaction BeginTransaction()
        {
            _context.Database.BeginTransaction();

            return _context.Database.CurrentTransaction.GetDbTransaction();
        }

        public void CommitTransaction()
        {
            _context.SaveChanges();
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true) where T : class
        {
            var _dbSet = _context.Set<T>();
            var iQueryable = isNoTracking ? _dbSet.Where(whereExpression).AsNoTracking() : _dbSet.Where(whereExpression);
            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;

            return await iQueryable.FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync<T>(IQueryable<T> iQueryable, string orderBy = "") where T : class
        {
            iQueryable = !string.IsNullOrEmpty(orderBy) ? iQueryable : iQueryable.OrderByBatch(orderBy);
            var data = await iQueryable.FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true) where T : class
        {
            if (whereExpression == null)
            {
                whereExpression = e => true;
            }
            var _dbSet = _context.Set<T>();
            var iQueryable = isNoTracking ? _dbSet.Where(whereExpression).AsNoTracking() : _dbSet.Where(whereExpression);
            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;
            return await iQueryable.ToListAsync();
        }
        public async Task<List<T>> GetListAsync<T>(IQueryable<T> iQueryable) where T : class
        {

            var _dbSet = _context.Set<T>();

            //  iQueryable = iQueryable.Skip((paginationBase.PageIndex - 1) * paginationBase.PageSize).Take(paginationBase.PageSize);

            var data = await iQueryable.ToListAsync();
            return data;
        }
        public async Task<PaginatedList<T>> GetPageAsync<T>(IQueryable<T> iQueryable, PaginationBase paginationBase) where T : class
        {
            var count = (await iQueryable.ToListAsync()).Count;

            int index = paginationBase.PageIndex - 1;
            int size = paginationBase.PageSize;

            var data = await iQueryable.OrderBy(paginationBase.OrderBy).Skip(index * size).Take(size).ToListAsync();

            var pageList = new PaginatedList<T>(paginationBase.PageIndex, paginationBase.PageSize, count, data);

            return await Task.Run(() => pageList);
        }

        public async Task<PaginatedList<T>> GetPageAsync<T>(PaginationBase paginationBase, Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false) where T : class
        {
            if (whereExpression == null)
            {
                whereExpression = e => true;
            }

            var iQueryable = _context.Set<T>().Where(whereExpression);

            var count = (await iQueryable.ToListAsync()).Count;

            int index = paginationBase.PageIndex - 1;
            int size = paginationBase.PageSize;

            iQueryable = isIgnoreQueryFilters ? iQueryable.IgnoreQueryFilters() : iQueryable;
            var data = await iQueryable.OrderBy(paginationBase.OrderBy).Skip(index * size).Take(size).ToListAsync();

            var pageList = new PaginatedList<T>(paginationBase.PageIndex, paginationBase.PageSize, count, data);

            return await Task.Run(() => pageList);

        }


    }


}
