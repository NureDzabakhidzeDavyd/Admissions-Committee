using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain.Filters;
using AdmissionsCommittee.Core.Enums;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Data.Helpers
{
    public class QueryBuilder : IQueryBuilder
    {
        public Query GetAllQuery { get; set; }
        public string TableName { get; set; }

        public virtual string PaginateFilter(PaginationFilter paginationFilter, SortFilter? sortFilter, DynamicFilters? dynamicFilters)
        {
            if (sortFilter?.Field is not null)
            {
                AddSorting(sortFilter);
            }
            else if (dynamicFilters?.Filters?.Count() > 0)
            {
                AddFilter(dynamicFilters);
            }
            AddPagination(paginationFilter);
            return ToString();
        }

        private void Reset()
        {
            GetAllQuery = AddQuery();
        }

        private Query AddQuery()
        {
            var query = new Query(TableName);
            return query;
        }

        private void AddFilter(DynamicFilters filters)
        {
            filters.Filters?.ToList().ForEach(AddFilter);
        }

        private void AddFilter(DynamicFilter dynamicFilter)
        {
            switch (dynamicFilter.FieldType)
            {
                case (int)FieldType.Number:
                case (int)FieldType.Text:
                    GetAllQuery.Where(dynamicFilter.FieldName, "=", dynamicFilter.Value);
                    break;
                default:
                    throw new ArgumentException($"{dynamicFilter.FieldType} doesn't exist");
            }
        }

        private void AddSorting(SortFilter sortFilter)
        {
            if (sortFilter.Descending)
            {
                GetAllQuery.OrderByDesc(sortFilter.Field);
            }
            else
            {
                GetAllQuery.OrderBy(sortFilter.Field);
            }
        }

        private void AddPagination(PaginationFilter paginationFilter)
        {
            GetAllQuery
                .Skip((paginationFilter.Page - 1) * paginationFilter.Size)
                .Take(paginationFilter.Size);
        }

        public override string ToString()
        {
            var result = MsSqlQueryToString(GetAllQuery);
            Reset();
            return result;
        }

        public string MsSqlQueryToString(Query query)
        {
            var compiler = new SqlServerCompiler();
            SqlResult sqlResult = compiler.Compile(query);
            return sqlResult.ToString();
        }
    }
}
