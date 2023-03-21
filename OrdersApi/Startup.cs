using MediatR;
using FluentValidation;
using System.Reflection;
using OrdersApi.Entities;
using OrdersApi.Installers;
using OrdersApi.Persistence;
using OrdersApi.Persistence.Abstract;
using OrdersApi.PipelineBehaviors;
using OrdersApi.Repositories;
using OrdersApi.Repositories.Abstract;
using OrdersApi.Repositories.Cached;
using OrdersApi.Strategies;
using OrdersApi.Strategies.Abstract;

namespace OrdersApi;

public class Startup
{
    public IConfiguration Configuration { get; }
    private const string PolicyName = "Application policy";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    // Add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.InstallSqliteDatabase(Configuration);
        services.AddScoped<IRepository<ProductEntity>, ProductRepository>();
        services.AddScoped<IRepository<CustomerEntity>,CustomerRepository>();
        services.AddScoped<IRepository<OrderEntity>, OrderRepository>();
        
        services.AddMemoryCache();
        services.Decorate<IRepository<OrderEntity>, CachedRepository<OrderEntity>>();
        services.Decorate<IRepository<CustomerEntity>, CachedRepository<CustomerEntity>>();
        services.Decorate<IRepository<ProductEntity>, CachedRepository<ProductEntity>>();
        services.AddScoped<IDataLoader, SqliteDataLoader>();
        
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

        services.AddScoped<IModelStateCreator, DefaultModelStateCreator>();
        services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
    
    // Configure the HTTP request pipeline.
    public async Task Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        
        app.UseDefaultFiles();
        app.UseStaticFiles();
        
        app.UseAuthorization();

        app.MapControllers();

        app.UseCors(PolicyName);

        if (Environment.GetEnvironmentVariable("DatabaseCreated") == null || Environment.GetEnvironmentVariable("DatabaseCreated") == "false")
        {
            app.InstallMigrations(app.Logger);
            await app.LoadDataAsync(app.Logger);
        }

        app.Run();
    }
}