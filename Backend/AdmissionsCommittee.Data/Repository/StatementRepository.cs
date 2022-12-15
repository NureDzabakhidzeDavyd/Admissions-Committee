using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain;
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
    public class StatementRepository : BaseRepository<Statement>, IStatementRepository
    {
        public StatementRepository(RepositoryConfiguration sqlConfiguration, IQueryBuilder queryBuilder) : base(sqlConfiguration, queryBuilder)
        {
        }

        public async Task<IEnumerable<Statement>> GetAllSpecialityStatementsAsync(int specialityId)
        {

            var statementColumn = $"{nameof(Statement.SpecialityId)}";
            var personTable = nameof(Person);
            var specialityTable = nameof(Speciality);

            var request = new Query(TableName)
                .Join(personTable, nameof(Person.PersonId), $"{TableName}.{nameof(Statement.ApplicantId)}")
                .Join(specialityTable, $"{specialityTable}.{nameof(Speciality.SpecialityId)}", $"{TableName}.{nameof(Statement.SpecialityId)}")
                .Where($"{TableName}.{statementColumn}", "=", specialityId);
            var query = QueryBuilder.MsSqlQueryToString(request);

            var statements = await Connection.QueryAsync<Statement, Person, Speciality, Statement>(query, 
                (statement, person, speciality) =>
            {
                statement.Applicant.Id = person.PersonId;
                statement.Applicant.Name = $"{person.FirstName} {person.SecondName}";
                statement.Speciality.Id = speciality.SpecialityId;
                statement.Speciality.Name = speciality.SpecialityName;
                return statement;
            }, splitOn: $"{nameof(Person.PersonId)}, {nameof(Speciality.SpecialityId)}");
            return statements;
        }

        public async Task<IEnumerable<Statement>> GetApplicantStatementsAsync(int applicantId)
        {
            var statementColumn = $"{nameof(Statement.ApplicantId)}";
            var personTable = nameof(Person);
            var specialityTable = nameof(Speciality);

            var sql = new Query(TableName)
                .Where($"{TableName}.{statementColumn}", "=", applicantId)
                .Join(personTable, nameof(Person.PersonId), $"{TableName}.{nameof(Statement.ApplicantId)}")
                .Join(specialityTable, $"{specialityTable}.{nameof(Speciality.SpecialityId)}", $"{TableName}.{nameof(Statement.SpecialityId)}");
            var query = QueryBuilder.MsSqlQueryToString(sql);

            var statements = await Connection.QueryAsync<Statement, Person, Speciality, Statement>(query,
                (statement, person, speciality) =>
                {
                    statement.Applicant.Id = person.PersonId;
                    statement.Applicant.Name = $"{person.FirstName} {person.SecondName}";
                    statement.Speciality.Id = speciality.SpecialityId;
                    statement.Speciality.Name = speciality.SpecialityName;
                    return statement;
                }, splitOn: $"{nameof(Person.PersonId)}, {nameof(Speciality.SpecialityId)}");
            return statements;
        }
    }
}
