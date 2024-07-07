using DWorks.Infra.IoC;
using DWorks.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterSwaggerConfig();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.RegisterApiVersion();
builder.Services.RegisterContexts(builder.Configuration);
builder.Services.RegisterRepositories();
builder.Services.RegisterQueries();
builder.Services.RegisterMediatR();
builder.Services.RegisterMapper();
builder.Services.RegisterUseCases();

var app = builder.Build();
app.UseSwaggerConfig(builder.Environment);
app.UseApiConfiguration();

app.Run();