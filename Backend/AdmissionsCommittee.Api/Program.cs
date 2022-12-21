using AdmissionsCommittee.Api.Installers;
using AdmissionsCommittee.Core.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Services.AddAutoMapper(typeof(Program));

builder.InstallServicesInAsembly();

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
