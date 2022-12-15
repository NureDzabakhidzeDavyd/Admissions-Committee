using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Options;
using SqlKata;
using System;
using Dapper.Contrib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dapper;
using AdmissionsCommittee.Data.Helpers;
using AdmissionsCommittee.Core.Domain.Filters;

namespace AdmissionsCommittee.Data.Repository
{
    public class BaseRepository<T> : IDisposable, IRepository<T> where T : class
    {
        protected readonly IDbConnection Connection;
        protected readonly string TableName = typeof(T).Name;
        protected readonly IQueryBuilder QueryBuilder;

        public BaseRepository(RepositoryConfiguration sqlConfiguration, IQueryBuilder queryBuilder)
        {
            var connection = sqlConfiguration.ConnectionString;
            Connection = new SqlConnection(connection);
            QueryBuilder = queryBuilder;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await Connection.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities)
        {
            await Connection.InsertAsync(entities);

            return entities;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Connection.UpdateAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            await Connection.DeleteAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entityToDel = await Connection.GetAsync<T>(id);
            await Connection.DeleteAsync(entityToDel);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var result = await Connection.GetAsync<T>(id);
            return result;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await Connection.GetAllAsync<T>();
            return result;
        }

        public async Task<IEnumerable<T>> GetManyByIdAsync(IEnumerable<int> ids, string columnName)
        {
            var columnId = columnName;
            var query = new Query(TableName).WhereIn(columnId, ids);
            var compileString = QueryBuilder.MsSqlQueryToString(query);

            var result = await Connection.QueryAsync<T>(compileString);
            return result;
        }

        public virtual async Task<IEnumerable<T>> PaginateAsync(PaginationFilter paginationFilter, SortFilter? sortFilter, DynamicFilters? dynamicFilters)
        {
            QueryBuilder.GetAllQuery = new Query(TableName);
            QueryBuilder.TableName = TableName;
            var query = QueryBuilder.PaginateFilter(paginationFilter, sortFilter, dynamicFilters);

            var result = await Connection.QueryAsync<T>(query);
            return result;
        }

        #region Dispose pattern

        private void ReleaseUnmanagedResources()
        {
            Connection.Close();
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                Connection.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}
