using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.Domain.Filters;
using AdmissionsCommittee.Core.Options;
using Dapper;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Data.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DapperContext dapperContext, IQueryBuilder queryBuilder) : base(dapperContext, queryBuilder)
        {
        }

        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var query = QueryBuilder.MsSqlQueryToString(GetAllQuery());
            return await GetAllAsync(query);
        }

        public override async Task<Employee> GetByIdAsync(int id)
        {
            var query = QueryBuilder.MsSqlQueryToString(GetByIdQuery(id));
            return (await GetAllAsync(query)).FirstOrDefault();
        }

        public override async Task<IEnumerable<Employee>> PaginateAsync(
            PaginationFilter paginationFilter,
            SortFilter? sortFilter,
            DynamicFilters? dynamicFilters)
        {
            QueryBuilder.GetAllQuery = GetAllQuery();
            QueryBuilder.TableName = nameof(Employee);

            var query = QueryBuilder.PaginateFilter(paginationFilter, sortFilter, dynamicFilters);
            return await GetAllAsync(query);
        }

        private async Task<IEnumerable<Employee>> GetAllAsync(string query)
        {
            var employees = await Connection.QueryAsync<Employee, Faculty, Person, Working, Rank, Employee>(query,
            (employee, faculty, person, working, rank) =>
            {
                employee.Faculty = faculty;
                employee.Person = person;
                if (rank != null)
                {
                    working.Rank = rank;
                    employee.Working.Add(working);
                }
                return employee;
            }, splitOn: $"{nameof(Faculty.FacultyId)}, {nameof(Person.PersonId)}, {nameof(Working.WorkingId)}, {nameof(Rank.RankId)}");


                var result = employees.GroupBy(p => p.EmployeeId).Select(g =>
                {
                    var groupedEmployee = g.First();
                    if(groupedEmployee.Working.Any())
                    {
                    groupedEmployee.Working = g.Select(p => p.Working.Single()).ToList();
                    }
                    return groupedEmployee;
                });
            return result;
        }

        private Query GetAllQuery()
        {
            var rankTableName = nameof(Rank);
            var personTableName = nameof(Person);
            var workingTableName = nameof(Working);
            var facultyTableName = nameof(Faculty);


            var query = new Query(TableName)
                .Join(facultyTableName, $"{facultyTableName}.{nameof(Faculty.FacultyId)}",
                    $"{TableName}.{nameof(Employee.FacultyId)}")
                .Join(personTableName, $"{personTableName}.{nameof(Person.PersonId)}",
                    $"{TableName}.{nameof(Employee.EmployeeId)}")
                .LeftJoin(workingTableName, $"{workingTableName}.{nameof(Working.EmployeeId)}",
                    $"{TableName}.{nameof(Employee.EmployeeId)}")
                .LeftJoin(rankTableName, $"{rankTableName}.{nameof(Rank.RankId)}",
                    $"{workingTableName}.{nameof(Working.RankId)}");

            return query;
        }

        private Query GetByIdQuery(int id)
        {
            var rankTableName = nameof(Rank);
            var personTableName = nameof(Person);
            var workingTableName = nameof(Working);
            var facultyTableName = nameof(Faculty);


            var query = new Query(TableName)
                .Join(facultyTableName, $"{facultyTableName}.{nameof(Faculty.FacultyId)}",
                    $"{TableName}.{nameof(Employee.FacultyId)}")
                .Join(personTableName, $"{personTableName}.{nameof(Person.PersonId)}",
                    $"{TableName}.{nameof(Employee.EmployeeId)}")
                .LeftJoin(workingTableName, $"{workingTableName}.{nameof(Working.EmployeeId)}",
                    $"{TableName}.{nameof(Employee.EmployeeId)}")
                .LeftJoin(rankTableName, $"{rankTableName}.{nameof(Rank.RankId)}",
                    $"{workingTableName}.{nameof(Working.RankId)}")
                .Where($"{TableName}.{nameof(Employee.EmployeeId)}", "=", id);

            return query;
        }
    }
}
