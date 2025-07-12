
using Microsoft.EntityFrameworkCore;
using SGM.Application.Contracts.Repositories.Insurance;
using SGM.Application.Contracts.Services.insuranse;
using SGM.Application.Services.Insurance;
using SGM.Persistence.Context;
using SGM.Persistence.Repositories;
using SGM.IOC.Dependencies.Insurance;

namespace SGM.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<HealtSyncContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HealtSyncConnection")));

            builder.Services.AddInsuranceDependency();

         
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
