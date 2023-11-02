using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenPositionsApp.Adapter;
using OpenPositionsApp.Interfaces;
using OpenPositionsApp.Models;
using OpenPositionsApp.Models.Context;
using OpenPositionsApp.Repositories;
using OpenPositionsApp.Services;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Text;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Service Configuration
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        #endregion

        #region DatabaseInjection
        builder.Services.AddDbContext<OpenPositionContext>(opts =>
        {
            opts.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
        });
        #endregion

        #region RepositoryInjection
        builder.Services.AddScoped<IBaseRepo<Location>, LocationRepo>();
        builder.Services.AddScoped<IPositionRepo<OpenPosition, int>, PositionRepo>();
        builder.Services.AddScoped<IAdapter, Adapter>();
        #endregion

        #region ServiceInjection
        builder.Services.AddScoped<IPositionService, PositionService>();
        #endregion

        #region Cors Policy
        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("ReactCors", options =>
            {
                options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });
        #endregion

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
        builder.Host.UseSerilog();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            #region SwaggerConfiguration
            app.UseSwagger();
            app.UseSwaggerUI();
            #endregion
        }

        #region MiddlewareConfiguration
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();
        app.UseCors("ReactCors");
        app.UseAuthentication();
        app.UseAuthorization();
        #endregion

        app.MapControllers();

        app.Run();
    }
}
