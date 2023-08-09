using Domain;
using Grpc.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using TestTask;
using TestTask.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Metrix API", Version = "v1" });
});

builder.Services.AddSignalR();
builder.Services.AddScoped<IMetricRepository, MetricRepository>();
builder.Services.AddScoped<IDiskSpaceRepository, DiskSpaceRepository>();
builder.Services.AddScoped<IMetrixService, MetrixService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Metrix API V1");

    });
}

app.MapHub<SignalRMetrix>("/MetrixHub");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();






