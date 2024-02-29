using APIOrquestracao.Clients;
using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ContagemClient>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseElasticApm(app.Configuration,
    new HttpDiagnosticsSubscriber());

app.MapControllers();

app.Run();