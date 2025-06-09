using api.Data;
using api.Interfaces;
using api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventsApiLambda;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        DotNetEnv.Env.Load();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var db = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var pass = Environment.GetEnvironmentVariable("DB_PASS");

        var connectionString = $"Server={host};Database={db};User Id={user};Password={pass};TrustServerCertificate=True;";
        Console.WriteLine("CS: " + connectionString);

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));


        services.AddAuthorization();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}