using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.Domain.Filters;
using AdmissionsCommittee.Core.Options;
using AdmissionsCommittee.Data.Helpers;
using Dapper;
using Dapper.Contrib.Extensions;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Data.Repository
{
    public class SpecialtyRepository : BaseRepository<Speciality>, ISpecialtyRepository
    {
        public SpecialtyRepository(RepositoryConfiguration sqlConfiguration, IQueryBuilder queryBuilder) : base(sqlConfiguration, queryBuilder)
        {
        }

        public override async Task<IEnumerable<Speciality>> GetAllAsync()
        {
            var query = QueryBuilder.MsSqlQueryToString(GetAllQuery());

            var speciality = await Connection.QueryAsync<Speciality, Faculty, Coefficient, Eie, Speciality>(query,
                (speciality, faculty, coef, eie) =>
                {
                    speciality.Faculty = faculty;
                    coef.Eie = eie;
                    speciality.Coefficients.Add(coef);

                    return speciality;
                }, splitOn: $"{nameof(Faculty.FacultyId)}, {nameof(Coefficient.CoefficientId)},  {nameof(Eie.EieId)}");

            var result = speciality.GroupBy(p => p.SpecialityId).Select(g =>
            {
                var groupedSpeciality = g.First();
                var temp = g.Select(p => p.Coefficients.First()).ToList();
                groupedSpeciality.Coefficients = temp;
                return groupedSpeciality;
            });
            return result;
        }

        public async override Task<IEnumerable<Speciality>> PaginateAsync(PaginationFilter paginationFilter, SortFilter? sortFilter, DynamicFilters? dynamicFilters)
        {
            QueryBuilder.GetAllQuery = GetAllQuery();
            QueryBuilder.TableName = nameof(Speciality);

            var query = QueryBuilder.PaginateFilter(paginationFilter, sortFilter, dynamicFilters);

            var speciality = await Connection.QueryAsync<Speciality, Faculty, Coefficient, Eie, Speciality>(query,
                (speciality,faculty, coef, eie) =>
                {
                    speciality.Faculty = faculty;
                    coef.Eie = eie;
                    speciality.Coefficients.Add(coef);
                   
                    return speciality;
                }, splitOn: $"{nameof(Faculty.FacultyId)}, {nameof(Coefficient.CoefficientId)},  {nameof(Eie.EieId)}");

            var result = speciality.GroupBy(p => p.SpecialityId).Select(g =>
            {
                var groupedSpeciality = g.First();
                var temp = g.Select(p => p.Coefficients.First()).ToList();
                groupedSpeciality.Coefficients = temp;
                return groupedSpeciality;
            });
            return result;
        }

        private Query GetAllQuery()
        {
            var facultyTableName = nameof(Faculty);
            var coefsTableName = nameof(Coefficient);
            var eieTableName = nameof(Eie);

            var query = new Query(TableName)
                .Join(facultyTableName, $"{facultyTableName}.{nameof(Faculty.FacultyId)}",
                    $"{TableName}.{nameof(Speciality.FacultyId)}")
                .Join(coefsTableName, $"{coefsTableName}.{nameof(Coefficient.SpecialityId)}",
                    $"{TableName}.{nameof(Speciality.SpecialityId)}")
                .Join(eieTableName, $"{eieTableName}.{nameof(Eie.EieId)}",
                    $"{coefsTableName}.{nameof(Coefficient.EieId)}");

            return query;
        }

        public async Task<CompetitiveScoreStatistic> CompareApplicantCompetitiveScore(int applicantCompetitiveScore, int specialityId)
        {

            var query = @"select AM.applicantid,
               Round(Sum(SpecCoefs.coefficientvalue * AM.MarkValue / 3), 2) as 'CompetitiveScore'
                from (select SpecialityId, CoefficientValue from Coefficient where SpecialityId = @specialityId) SpecCoefs
                inner join (select S.SpecialityId, Mark.ApplicantId, Mark.MarkValue from Mark
                inner join Statement on Mark.ApplicantId = Statement.ApplicantId
                inner join Speciality S on S.SpecialityId = Statement.SpecialityId
                where S.SpecialityId = @specialityId) AM on SpecCoefs.SpecialityId = AM.SpecialityId
                group by AM.applicantid";
            var values = new { specialityId = specialityId };
            var applicantsCompetitiveScores = (await Connection.QueryAsync<ApplicantCompetitiveScore>(query, values, commandType: System.Data.CommandType.Text)).Select(x => x.CompetitiveScore).ToList();

            var averageCompetitiveScore = applicantsCompetitiveScores.Average();
            
            applicantsCompetitiveScores.Add(applicantCompetitiveScore);
            applicantsCompetitiveScores.Sort();
            applicantsCompetitiveScores.Reverse();
            //var position = applicantsCompetitiveScores.BinarySearch(applicantCompetitiveScore);

            //if (position == -1)
            //{
            //    throw new Exception("Can't find rating of applicant competitive score");
            //}

            var result = new CompetitiveScoreStatistic()
            {
                AverageCompetitiveScore = averageCompetitiveScore,
                TotalApplicantsCount = applicantsCompetitiveScores.Count - 1,
                ApplicantCompetitiveScorePosition = applicantsCompetitiveScores.IndexOf(applicantCompetitiveScore) + 1,
            };

            return result;
        }

        public async Task<IEnumerable<Speciality>> GetSpecialitiesInformationsAsync()
        {
            var result = await Connection.GetAllAsync<Speciality>();
            return result;
        }

        public override async Task<Speciality> GetByIdAsync(int id)
        {
            var sql = GetAllQuery().Where($"{TableName}.{nameof(Speciality.SpecialityId)}", "=", id);
            var query = QueryBuilder.MsSqlQueryToString(sql);

            var speciality = await Connection.QueryAsync<Speciality, Faculty, Coefficient, Eie, Speciality>(query,
                (speciality, faculty, coef, eie) =>
                {
                    speciality.Faculty = faculty;
                    coef.Eie = eie;
                    speciality.Coefficients.Add(coef);

                    return speciality;
                }, splitOn: $"{nameof(Faculty.FacultyId)}, {nameof(Coefficient.CoefficientId)},  {nameof(Eie.EieId)}");

            var result = speciality.GroupBy(p => p.SpecialityId).Select(g =>
            {
                var groupedSpeciality = g.First();
                var temp = g.Select(p => p.Coefficients.First()).ToList();
                groupedSpeciality.Coefficients = temp;
                return groupedSpeciality;
            });

            return result.Single();
        }
    }
}
