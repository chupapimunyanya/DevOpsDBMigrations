using Microsoft.EntityFrameworkCore;
using UserApi.Data;

namespace UserApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


            if (!string.IsNullOrEmpty(connectionString) &&
               (connectionString.Contains("Host=", StringComparison.OrdinalIgnoreCase) ||
                connectionString.Contains("User ID=", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("--> Using PostgreSQL Database (Cloud)");
                builder.Services.AddDbContext<UserDbContext>(options =>
                    options.UseNpgsql(connectionString));
            }
            else
            {
                Console.WriteLine("--> Using SQLite Database (Local)");
                builder.Services.AddDbContext<UserDbContext>(options =>
                    options.UseSqlite(connectionString));
            }

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<UserDbContext>();
                    context.Database.Migrate();

                    if (!context.Users.Any())
                    {
                        context.Users.AddRange(
                            new UserApi.Models.User { Username = "cloud_user", Email = "cloud@test.com" },
                            new UserApi.Models.User { Username = "admin_user", Email = "admin@test.com" }
                        );
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error during migration.");
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI(); 

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}