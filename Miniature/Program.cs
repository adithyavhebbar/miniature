
using Miniature.Repository;
using Miniature.Services;
using Miniature.Services.Interfaces;

namespace Miniature
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            
            builder.Services.AddSingleton<ICachedService>(x => new CachedService(x.GetRequiredService<IConfiguration>()));
            builder.Services.AddSingleton<IShortenService>(x => new ShortenerService(x.GetRequiredService<ICachedService>()));
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
