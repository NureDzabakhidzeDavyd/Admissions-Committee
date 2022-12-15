using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Domain;
using Bogus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Data
{
    public class Seeder : ISeeder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Seeder> _logger;

        public Seeder(IUnitOfWork unitOfWork, ILogger<Seeder> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            var specialty = await _unitOfWork.SpecialtyRepository.GetSpecialitiesInformationsAsync();
            if (!specialty.Any())
            {
                await GenerateEieAsync();
                await GenerateRankAsync();
                await GenerateFacultyAsync();

                await GenerateEmployeeAsync(30);
                await GenerateWorkingAsync();

                await GenerateApplicantAsync(60);

                await GenerateSpecialtyAsync();
                await GenerateSpecialityCoefficientAsync();
                await GenerateSpecialityStatisticsAsync();
                await GenerateStatementAsync();
            }
        }

        private async Task GenerateApplicantMarksAsync(IEnumerable<Statement> statements, IEnumerable<Coefficient> coefficients)
        {
            _logger.LogInformation("Creating marks");
            var marks = new List<Mark>();        
          
            foreach (var statement in statements)
            {
                var specialityCoeffs = coefficients.Where(x => x.SpecialityId == statement.SpecialityId);
                foreach (var coef in specialityCoeffs)
                {
                    var currentYear = DateTime.Now.Year;
                    var fakeMarks = new Faker<Mark>()
                    .RuleFor(x => x.ApplicantId, statement.ApplicantId)
                    .RuleFor(x => x.EieId, faker => coef.EieId)
                    .RuleFor(x => x.WriteYear, faker => faker.Random.Int(currentYear - 3, currentYear))
                    .RuleFor(x => x.MarkValue, faker => faker.Random.Int(125, 200))
                    .Generate();
                    marks.Add(fakeMarks);
                }
            }

            await _unitOfWork.MarkRepository.CreateManyAsync(marks);
            _logger.LogInformation("Marks are created");
        }

        private async Task GenerateSpecialityStatisticsAsync()
        {
            _logger.LogInformation("Creating speciality's statistics");
            var speciality = (await _unitOfWork.SpecialtyRepository.GetAllAsync()).ToList();
            var statistics = new List<Statistic>();
           speciality.ForEach(speciality =>
            {
                var fakeStatistics = new Faker<Statistic>()
                .RuleFor(x => x.SpecialityId, speciality.SpecialityId)
                .RuleFor(x => x.StatisticYear, faker => faker.Random.Int(2017, DateTime.UtcNow.Year))
                .RuleFor(x => x.BudgetMin, faker => MathF.Round(faker.Random.Float(150f, 170f), 2))
                .RuleFor(x => x.BudgetAver, (faker, o) => MathF.Round(o.BudgetMin + faker.Random.Float(10.0f, 20f),2))
                .RuleFor(x => x.ContractMin, faker => MathF.Round(faker.Random.Float(125f, 150f), 2))
                .RuleFor(x => x.ContractAver, (faker, o) => MathF.Round(o.ContractMin + faker.Random.Float(10.0f, 20f), 2))
                .Generate(3);
                statistics.AddRange(fakeStatistics);
            });
            await _unitOfWork.StatisticRepository.CreateManyAsync(statistics);
            _logger.LogInformation("Speciality's statistics are created");
        }

        private async Task GenerateSpecialityCoefficientAsync()
        {
            _logger.LogInformation("Creating speciality's coefficients");
            var coefs = new List<Coefficient>();
            var speciality = await _unitOfWork.SpecialtyRepository.GetSpecialitiesInformationsAsync();
            var eies = await _unitOfWork.EieRepository.GetAllAsync();
            var variousCoefs = new[]
            {
                new float[] {0.5f, 0.2f, 0.3f},
                new float[] {0.5f, 0.3f, 0.2f},
                new float[] {0.4f, 0.3f, 0.3f},
                new float[] {0.4f, 0.4f, 0.2f},
            };

            

            foreach (var special in speciality)
            {
                var faker = new Faker();
                var randomCoefs = faker.PickRandom(variousCoefs);
                var randomEie = (faker.PickRandom(eies, 3)).ToArray();
                for (int i = 0; i < 3; i++)
                {
                    var coef = new Faker<Coefficient>()
                                        .RuleFor(x => x.CoefficientValue, MathF.Round(randomCoefs[i], 2))
                                        .RuleFor(x => x.SpecialityId, special.SpecialityId)
                                        .RuleFor(x => x.EieId, randomEie[i].EieId)
                                        .Generate();
                    coefs.Add(coef);
                }
            }

                await _unitOfWork.CoefficientRepository.CreateManyAsync(coefs);

            _logger.LogInformation("Coeffs are created");
        }

        private async Task GenerateEieAsync()
        {
            _logger.LogInformation("Creating eie");
            var eieNames = new[]
            {
                "Українська мова",
                "Математика",
                "Фізика",
                "Хімія",
                "Географія",
                "Українська література",
                "Біологія",
                "Історія України",
                "Іноземна мова"
            };

            var eie = eieNames.Select(eieName =>
            {
                return new Eie()
                {
                    EieName = eieName
                };
            });
            await _unitOfWork.EieRepository.CreateManyAsync(eie);
            _logger.LogInformation("Eie are created");
        }

        private async Task GenerateStatementAsync()
        {
            _logger.LogInformation("Creating statements");
            var applicants = await _unitOfWork.ApplicantRepository.GetAllAsync();
            var specialtyCoeffs = await _unitOfWork.CoefficientRepository.GetAllAsync();

            var statements = applicants.Select(person =>
            {
                return new Faker<Statement>()
                .RuleFor(x => x.ApplicantId, person.ApplicantId)
                .RuleFor(x => x.SpecialityId, faker => faker.PickRandom(specialtyCoeffs).SpecialityId)
                .Generate();
            });

            await _unitOfWork.StatementRepository.CreateManyAsync(statements);

            _logger.LogInformation("Statements are created");

            await GenerateApplicantMarksAsync(statements, specialtyCoeffs);
        }

        private async Task GenerateApplicantAsync(int count)
        {
            _logger.LogInformation("Creating applicants");
            var createdPersons = await GeneratePersonsAsync(count);
            var persons = await _unitOfWork.PersonRepository.GetAllAsync();

            var applicants = persons.Select(person =>
            {
                return new Faker<Applicant>()
                .RuleFor(x => x.ApplicantId, person.PersonId)
                // TODO: Rebaundant certificate
                .RuleFor(x => x.Certificate, faker => MathF.Round(faker.Random.Float(), 1))
                .Generate();
            });

            await _unitOfWork.ApplicantRepository.CreateManyAsync(applicants);
            _logger.LogInformation("Applicants are created");
        }

        private async Task GenerateWorkingAsync()
        {
            _logger.LogInformation("Creating working");
            var employees = (await _unitOfWork.EmployeeRepository.GetAllAsync()).ToList();
            var ranks = (await _unitOfWork.RankRepository.GetAllAsync()).ToList();

            var working = new List<Working>();
            employees.ForEach(employee =>
            {
                var fakeWorking = new Faker<Working>()
               .RuleFor(x => x.RankId, faker => faker.PickRandom(ranks).RankId)
               .RuleFor(x => x.EmployeeId, employee.EmployeeId)
               .RuleFor(x => x.IssuedYear, faker => faker.Random.Int(1995, DateTime.Now.Year))
               .Generate();

                working.Add(fakeWorking);
            });

            await _unitOfWork.WorkingRepository.CreateManyAsync(working);
            _logger.LogInformation("Working are created");
        }

        private async Task GenerateEmployeeAsync(int count)
        {
            var createdPersons = await GeneratePersonsAsync(count);

            _logger.LogInformation("Creating employees");
            var faculties = await _unitOfWork.FacultyRepository.GetAllAsync();
            var getAllPersons = await _unitOfWork.PersonRepository.GetAllAsync();
            var fakeEmployees = getAllPersons.Select(person => new Faker<Employee>()
            .RuleFor(x => x.EmployeeId, person.PersonId)
            .RuleFor(x => x.FacultyId, f => f.PickRandom(faculties).FacultyId)
            .Generate()
            );
            var employees = await _unitOfWork.EmployeeRepository.CreateManyAsync(fakeEmployees);
            _logger.LogInformation("employees are created");
        }

        private async Task GenerateRankAsync()
        {
            _logger.LogInformation("Creating ranks");

            var ranksObjects = new List<Rank>();
            var ranks = new List<string>()
            {
                "Старший викладач кафедри програмної інженерії",
                "Зав. кафедри програмної інженерії",
                "Доцент кафедри програмної інженерії",

                "Декан факультету комп'ютерних наук",
                "Заступник голови приймальної комісії",
                "Член НМР",
                "Член НТР",
                "Член Координаційної Ради Асоціації випускників ХНУРЕ",
                "Професор кафедри ПІ",
                "Доктор технічних наук",
                "кандидат технічних наук",
                "Доцент",

                "Зав. кафедри штучного інтелекту",
                "Професор кафедри штучного інтелекту",
                "Доцент кафедри штучного інтелекту",
                "Старший викладач кафедри штучного інтелекту",
                "Заступник декана факультету КН",

                "Професор кафедри інформаційних управляючих систем",
                "Старший викладач кафедри інформаційних управляючих систем",
                "Доцент кафедри інформаційних управляючих систем",
                "Зав. кафедри штучного інтелекту",
            };

            ranks.ForEach(rankName => ranksObjects.Add(new Rank() { RankName = rankName }));
            await _unitOfWork.RankRepository.CreateManyAsync(ranksObjects);

            _logger.LogInformation("Faculties are created");
        }

        public async Task GenerateFacultyAsync()
        {
            _logger.LogInformation("Creating faculties");

            var faculties = new List<Faculty>();
            var facultiesList = new List<string>()
            {
                "Факультет комп’ютерних наук",
                "Факультет комп’ютерної інженерії та управління",
                "Факультет автоматики і комп’ютеризованих технологій",
                "Факультет інформаційно-аналітичних технологій та менеджменту",
                "Факультет інфокомунікацій",
                "Факультет електронної та біомедичної інженерії",
                "Факультет інформаційних радіотехнологій та технічного захисту інформації"
            };

            facultiesList.ForEach(facultyName => faculties.Add(new Faculty() { FacultyName = facultyName }));

            await _unitOfWork.FacultyRepository.CreateManyAsync(faculties);

            _logger.LogInformation("Faculties are created");
        }

        public async Task GenerateSpecialtyAsync()
        {
            _logger.LogInformation("Creating specialty");
            var faculties = await _unitOfWork.FacultyRepository.GetAllAsync();
            var specialty = new List<Speciality>()
            {
                new Speciality()
                {
                    SpecialityCode = 121,
                    EducationalProgram = "Програмна інженерія",
                    FacultyId = faculties.First(x => x.FacultyName == "Факультет комп’ютерних наук").FacultyId,
                    SpecialityName = "Інженерія програмного забезпечення",
                    EducationDegree = "Бакалавр",
                    BranchName = "Інформаційні технології",
                    OfferType = "Відкрита (в т.ч. для іноземців) (із вказанням пріоритетності)",
                    EducationForm = "Денна",
                    EducationCost = 17900,
                    SeatTotal = 255,
                    SubmittedApplicationsTotal = 758,
                    BudgetTotal = 204,
                    ContractTotal = 51,
                    Quota1Total = 10,
                    Quota2Total = 82
                },
                new Speciality()
                {
                    SpecialityCode = 122,
                    EducationalProgram = "Інформатика",
                    FacultyId = faculties.First(x => x.FacultyName == "Факультет інформаційно-аналітичних технологій та менеджменту").FacultyId,
                    SpecialityName = "Комп'ютерні науки",
                    EducationDegree = "Бакалавр",
                    BranchName = "Інформаційні технології",
                    OfferType = "Відкрита (в т.ч. для іноземців) (із вказанням пріоритетності)",
                    EducationForm = "Денна",
                    EducationCost = 17900,
                    SeatTotal = 100,
                    SubmittedApplicationsTotal = 259,
                    BudgetTotal = 67,
                    ContractTotal= 33,
                    Quota1Total = 4,
                    Quota2Total = 27
                },
                new Speciality()
                {
                    SpecialityCode = 126,
                    EducationalProgram = "Інформаційні системи та технології",
                    FacultyId = faculties.First(x => x.FacultyName == "Факультет інфокомунікацій").FacultyId,
                    SpecialityName = " Інформаційні системи та технології",
                    EducationDegree = "Бакалавр",
                    BranchName = "Інформаційні технології",
                    OfferType = "Відкрита (в т.ч. для іноземців) (із вказанням пріоритетності)",
                    EducationForm = "Денна",
                    EducationCost = 17900,
                    SeatTotal = 35,
                    SubmittedApplicationsTotal = 92,
                    BudgetTotal = 5,
                    ContractTotal= 30,
                    Quota1Total = 1,
                    Quota2Total = 2
                },
                new Speciality()
                {
                    SpecialityCode = 125,
                    EducationalProgram = "Безпека інформаційних і комунікаційних систем",
                    FacultyId = faculties.First(x => x.FacultyName == "Факультет комп’ютерної інженерії та управління").FacultyId,
                    SpecialityName = " Кібербезпека",
                    EducationDegree = "Бакалавр",
                    BranchName = "Інформаційні технології",
                    OfferType = "Відкрита (в т.ч. для іноземців) (із вказанням пріоритетності)",
                    EducationForm = "Денна",
                    EducationCost = 17900,
                    SeatTotal = 147,
                    SubmittedApplicationsTotal = 279,
                    BudgetTotal = 48,
                    ContractTotal= 99,
                    Quota1Total = 2,
                    Quota2Total = 19
                },
                new Speciality()
                {
                    SpecialityCode = 123,
                    EducationalProgram = "Комп’ютерна інженерія",
                    FacultyId = faculties.First(x => x.FacultyName == "Факультет комп’ютерної інженерії та управління").FacultyId,
                    SpecialityName = " Комп’ютерна інженерія",
                    EducationDegree = "Бакалавр",
                    BranchName = "Інформаційні технології",
                    OfferType = "Відкрита (в т.ч. для іноземців) (із вказанням пріоритетності)",
                    EducationForm = "Денна",
                    EducationCost = 17900,
                    SeatTotal = 250,
                    SubmittedApplicationsTotal = 550,
                    BudgetTotal = 189,
                    ContractTotal= 61,
                    Quota1Total = 11,
                    Quota2Total = 76
                }
            };
             await _unitOfWork.SpecialtyRepository.CreateManyAsync(specialty);

            _logger.LogInformation("Specialty are created");
        }

        private async Task<IEnumerable<Core.Domain.Person>> GeneratePersonsAsync(int count)
        {
            var persons = new Faker<Core.Domain.Person>(locale: "uk")
               .RuleFor(x => x.FirstName, (f => f.Name.FirstName()))
               .RuleFor(x => x.SecondName, f => f.Name.LastName())
               .RuleFor(x => x.Birth, f => f.Person.DateOfBirth.Date)
               .RuleFor(x => x.Address, f => $"{f.Address.StreetAddress()} м. {f.Address.City()}")
               .RuleFor(x => x.Email, (f, x) => f.Internet.Email(x.FirstName, x.SecondName))
               .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
               .RuleFor(x => x.Patronymic, f => f.Name.FirstName())
               .Generate(count);

            var createdPersons = await _unitOfWork.PersonRepository.CreateManyAsync(persons);

            return createdPersons;
        }
    }
}