
using PointSystem.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace PointSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
           
            // Add Swagger xml 주석 설정
            builder.Services.AddSwaggerGen( c=>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title="Point System", Version="버전 1.0", });
                
            });

            // DB추가
            builder.Services.AddDbContext<UsersContext>(options =>
              options.UseSqlServer("Server=D662-ETHANLIM;Database=Test06;Trusted_Connection=True;TrustServerCertificate=True"));

            

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

            app.Run();
        }
    }
}
