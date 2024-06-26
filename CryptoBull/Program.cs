
using CryptoBull.Data;
using CryptoBull.Services;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace CryptoBull
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services
                    .AddEndpointsApiExplorer()
                    .AddSwaggerGen()
                    .AddDbContext<AppDbContext>()
                    .AddHttpClient()
                    .AddScoped<IDbService, DbService>()
                    .AddScoped<ICryptoService, CryptoService>();


            builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
            {
                build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
            })); 
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("corspolicy");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
