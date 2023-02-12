using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.Domain.Filters;
using AdmissionsCommittee.Core.Options;
using Dapper;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandbookActivity.Core.Domain;

namespace AdmissionsCommittee.Data.Repository
{
    public class ApplicantRepository : BaseRepository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(DapperContext dapperContext, IQueryBuilder queryBuilder) : base(dapperContext, queryBuilder)
        {
        }

        public override async Task<Page<Applicant>> PaginateAsync(PaginationFilter paginationFilter, SortFilter? sortFilter, DynamicFilters? dynamicFilters)
        {
            var personTableName = nameof(Person);

            QueryBuilder.GetAllQuery = new Query(TableName)
                .Join(personTableName, $"{personTableName}.{nameof(Person.PersonId)}", $"{TableName}.{nameof(Applicant.ApplicantId)}");
            ;
            QueryBuilder.TableName = nameof(Employee);

            var query = QueryBuilder.PaginateFilter(paginationFilter, sortFilter, dynamicFilters);
            var data = await Connection.QueryAsync<Applicant, Person, Applicant>(query, (applicant, person) =>
            {
                applicant.Person = person;
                return applicant;
            }, splitOn: nameof(Person.PersonId));

            var count = await CountAsync();
            return new Page<Applicant>(data, count);
        }

        public override async Task<Applicant> GetByIdAsync(int id)
        {
            var personTableName = nameof(Person);
            var sql = new Query(TableName).Where(nameof(Applicant.ApplicantId), "=", id)
                .Join(personTableName, $"{personTableName}.{nameof(Person.PersonId)}", $"{TableName}.{nameof(Applicant.ApplicantId)}");
            var query = QueryBuilder.MsSqlQueryToString(sql);

            var applicant = (await Connection.QueryAsync<Applicant, Person, Applicant>(query, (applicant, person) =>
            {
                applicant.Person = person;
                return applicant;
            }, splitOn: nameof(Person.PersonId))).First();

            return applicant;
        }

        public override async Task<IEnumerable<Applicant>> GetAllAsync()
        {
            var personTableName = nameof(Person);

            var sql = new Query(TableName)
                .Join(personTableName, $"{personTableName}.{nameof(Person.PersonId)}", $"{TableName}.{nameof(Applicant.ApplicantId)}");
            ;
            var query = QueryBuilder.MsSqlQueryToString(sql);

            var applicants = await Connection.QueryAsync<Applicant, Person, Applicant>(query, (applicant, person) =>
            {
                applicant.Person = person;
                return applicant;
            }, splitOn: nameof(Person.PersonId));

            return applicants;
        }

        public async Task<float> CalculateApplicantCompetitiveScore(int applicantId, int specialityId)
        {
            // TODO: SEt values
            var values = new { applicantId = applicantId, specialityId = specialityId };
            var result = (await Connection.QueryAsync<float?>("SELECT dbo.CalculateApplicantCompetitiveScore(@applicantId, @specialityId)", new { applicantId = applicantId, specialityId = specialityId }, commandType: CommandType.Text)).Single();
            if(!result.HasValue)
            {
                throw new ArgumentNullException("Marks or Speciality are incorrect");
            }
            return (float)result;
        }
    }
}
