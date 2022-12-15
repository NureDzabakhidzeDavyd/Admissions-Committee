using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Options;
using AdmissionsCommittee.Data;
using AdmissionsCommittee.Data.Helpers;
using AdmissionsCommittee.Data.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors(options =>
{
    options.AddPolicy("angularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("*");
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var databaseConfiguration = builder.Configuration.GetSection(nameof(RepositoryConfiguration)).Get<RepositoryConfiguration>();
builder.Services.AddSingleton(databaseConfiguration);

builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRankRepository, RankRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IWorkingRepository, WorkingRepository>();
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<IStatementRepository, StatementRepository>();
builder.Services.AddScoped<IEieRepository, EieRepository>();
builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();
builder.Services.AddScoped<ICoefficientRepository, CoefficientRepository>();
builder.Services.AddScoped<IMarkRepository, MarkRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISeeder, Seeder>();
builder.Services.AddScoped<IQueryBuilder, QueryBuilder>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
    await seeder.SeedAsync();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("angularApp");

app.UseAuthorization();

app.UseEndpoints(endpoint => { endpoint.MapControllers(); });
app.Run();
