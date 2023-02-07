using Application;
using Application.BackgroundJobs;
using Infrastructure.Database;

namespace CartApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDatabaseInfrastructureLayer(builder.Configuration);
        builder.Services.AddApplicationLayer(builder.Configuration);

        builder.Services.AddHostedService<KafkaAddToCartBackgroundJob>();
        builder.Services.AddHostedService<KafkaRemoveFromCartBackgroundJob>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(policyBuilder =>
           policyBuilder.AddDefaultPolicy(policy =>
               policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
           );

        var app = builder.Build();           

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors();

        app.Run();
    }
}