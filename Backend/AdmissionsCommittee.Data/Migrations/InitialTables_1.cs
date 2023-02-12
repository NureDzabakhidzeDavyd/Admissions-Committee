using AdmissionsCommittee.Core.Domain;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace AdmissionsCommittee.Data.Migrations;

[Migration(1)]
public class InitialTables_1 : Migration
{
    public override void Down()
    {
        Delete.Table(nameof(Eie));
        Delete.Table(nameof(Faculty));
        Delete.Table(nameof(Rank));
        Delete.Table(nameof(Person));
        Delete.Table(nameof(Speciality));
        Delete.Table(nameof(Statistic));
        Delete.Table(nameof(Applicant));
        Delete.Table(nameof(Statement));
        Delete.Table(nameof(Mark));
        Delete.Table(nameof(Coefficient));
        Delete.Table(nameof(Employee));
        Delete.Table(nameof(Working));
    }
    public override void Up()
    {
        Create.Table(nameof(Eie))
            .WithColumn(nameof(Eie.EieId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Eie.Name)).AsString(50).NotNullable();
        
        Create.Table(nameof(Faculty))
            .WithColumn(nameof(Faculty.FacultyId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Faculty.Name)).AsString(100).NotNullable();
        
        Create.Table(nameof(Rank))
            .WithColumn(nameof(Rank.RankId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Rank.Name)).AsString(100).NotNullable();

        Create.Table(nameof(Person))
            .WithColumn(nameof(Person.PersonId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Person.FirstName)).AsString(30).NotNullable()
            .WithColumn(nameof(Person.SecondName)).AsString(30).NotNullable()
            .WithColumn(nameof(Person.Patronymic)).AsString(30).NotNullable()
            .WithColumn(nameof(Person.Address)).AsString(100).NotNullable()
            .WithColumn(nameof(Person.Birth)).AsDate().NotNullable()
            .WithColumn(nameof(Person.Email)).AsString(80).NotNullable()
            .WithColumn(nameof(Person.Phone)).AsString(30).NotNullable();


        Create.Table(nameof(Speciality))
            .WithColumn(nameof(Statistic.SpecialityId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Speciality.Name)).AsString(50).NotNullable()
            .WithColumn(nameof(Speciality.EducationalProgram)).AsString(80).NotNullable()
            .WithColumn(nameof(Speciality.EducationDegree)).AsString(50).NotNullable()
            .WithColumn(nameof(Speciality.Code)).AsInt32().NotNullable()
            .WithColumn(nameof(Speciality.BranchName)).AsString(60).NotNullable()
            .WithColumn(nameof(Speciality.FacultyId)).AsInt32().ForeignKey(nameof(Faculty), nameof(Faculty.FacultyId)).NotNullable()
            .WithColumn(nameof(Speciality.OfferType)).AsString(80).NotNullable()
            .WithColumn(nameof(Speciality.EducationForm)).AsString(60).NotNullable()
            .WithColumn(nameof(Speciality.EducationCost)).AsInt32().NotNullable()
            .WithColumn(nameof(Speciality.SeatTotal)).AsInt32().NotNullable()
            .WithColumn(nameof(Speciality.SubmittedApplicationsTotal)).AsInt32().NotNullable()
            .WithColumn(nameof(Speciality.BudgetTotal)).AsInt32().NotNullable()
            .WithColumn(nameof(Speciality.ContractTotal)).AsInt32().NotNullable()
            .WithColumn(nameof(Speciality.Quota1Total)).AsInt32().NotNullable()
            .WithColumn(nameof(Speciality.Quota2Total)).AsInt32().NotNullable();

        Create.Table(nameof(Statistic))
            .WithColumn(nameof(Statistic.StatisticId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Statistic.SpecialityId)).AsInt32().ForeignKey(nameof(Speciality), nameof(Speciality.SpecialityId)).NotNullable()
            .WithColumn(nameof(Statistic.BudgetMin)).AsInt32().NotNullable()
            .WithColumn(nameof(Statistic.StatisticYear)).AsInt32().NotNullable()
            .WithColumn(nameof(Statistic.BudgetAver)).AsInt32().NotNullable()
            .WithColumn(nameof(Statistic.ContractMin)).AsInt32().NotNullable()
            .WithColumn(nameof(Statistic.ContractAver)).AsInt32().NotNullable();

        Create.Table(nameof(Applicant))
            .WithColumn(nameof(Applicant.ApplicantId)).AsInt32().NotNullable().PrimaryKey().ForeignKey(nameof(Person), nameof(Person.PersonId))
            .WithColumn(nameof(Applicant.Certificate)).AsFloat().Nullable();

        Create.Table(nameof(Statement))
            .WithColumn(nameof(Statement.StatementId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Statement.SpecialityId)).AsInt32().ForeignKey(nameof(Speciality), nameof(Speciality.SpecialityId)).NotNullable()
            .WithColumn(nameof(Statement.ApplicantId)).AsInt32().ForeignKey(nameof(Applicant), nameof(Applicant.ApplicantId)).NotNullable();

        Create.Table(nameof(Mark))
            .WithColumn(nameof(Mark.MarkId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Mark.ApplicantId)).AsInt32().ForeignKey(nameof(Applicant), nameof(Applicant.ApplicantId)).NotNullable()
            .WithColumn(nameof(Mark.EieId)).AsInt32().ForeignKey(nameof(Eie), nameof(Eie.EieId)).NotNullable()
            .WithColumn(nameof(Mark.MarkValue)).AsInt32().NotNullable()
            .WithColumn(nameof(Mark.WriteYear)).AsInt32().NotNullable();

        Create.Table(nameof(Coefficient))
            .WithColumn(nameof(Coefficient.CoefficientId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Coefficient.EieId)).AsInt32().ForeignKey(nameof(Eie), nameof(Eie.EieId)).NotNullable()
            .WithColumn(nameof(Coefficient.SpecialityId)).AsInt32().ForeignKey(nameof(Speciality), nameof(Speciality.SpecialityId)).NotNullable()
            .WithColumn(nameof(Coefficient.CoefficientValue)).AsFloat().NotNullable();

        Create.Table(nameof(Employee))
            .WithColumn(nameof(Employee.EmployeeId)).AsInt32().NotNullable().PrimaryKey().ForeignKey(nameof(Person), nameof(Person.PersonId))
            .WithColumn(nameof(Employee.FacultyId)).AsInt32().ForeignKey(nameof(Faculty), nameof(Faculty.FacultyId)).NotNullable()
            .WithColumn(nameof(Employee.CareerInfo)).AsString(100).Nullable();

        Create.Table(nameof(Working))
            .WithColumn(nameof(Working.WorkingId)).AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Working.RankId)).AsInt32().ForeignKey(nameof(Rank), nameof(Rank.RankId)).NotNullable()
            .WithColumn(nameof(Working.EmployeeId)).AsInt32().ForeignKey(nameof(Employee), nameof(Employee.EmployeeId)).NotNullable()
            .WithColumn(nameof(Working.IssuedYear)).AsInt32().NotNullable();
    }
}