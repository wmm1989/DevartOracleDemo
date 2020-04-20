using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JQ.Common.Model;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace JQ.Common.IRepository
{
    public interface IUnitOfWork
    {
        DbContext GetDbContext();

        //  Task<bool> SaveChangesAsync();

        DbTransaction BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        Task<T> GetAsync<T>(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true) where T : class;

        Task<T> GetAsync<T>(IQueryable<T> iQueryable, string orderBy = "") where T : class;

        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> whereExpression, bool isIgnoreQueryFilters = false, bool isNoTracking = true) where T : class;
        Task<List<T>> GetListAsync<T>(IQueryable<T> iQueryable) where T : class;
        Task<PaginatedList<T>> GetPageAsync<T>(IQueryable<T> iQueryable, PaginationBase paginationBase) where T : class;

        Task<PaginatedList<T>> GetPageAsync<T>(PaginationBase paginationBase, Expression<Func<T, bool>> whereExpression = null, bool isIgnoreQueryFilters = false) where T : class;


    }



}
