using APIContagem.Data;
using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ContagemRepository>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<ContagemContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("BaseContagem")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseElasticApm(app.Configuration,
    new HttpDiagnosticsSubscriber(),
    new EfCoreDiagnosticsSubscriber());

app.MapControllers();

app.Run();