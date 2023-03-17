using System.Reflection;
using FluentValidation;
using MediatR;
using OrdersApi.Entities;
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

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    // Add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMemoryCache();
        
        services.AddSingleton<IRepository<ProductEntity>, InMemoryProductRepository>();
        services.AddSingleton<IRepository<CustomerEntity>,InMemoryCustomerRepository>();
        services.AddSingleton<IRepository<OrderEntity>, InMemoryOrderRepository>();
        
        services.Decorate<IRepository<OrderEntity>, CachedRepository<OrderEntity>>();
        services.Decorate<IRepository<CustomerEntity>, CachedRepository<CustomerEntity>>();
        services.Decorate<IRepository<ProductEntity>, CachedRepository<ProductEntity>>();
        
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

        services.AddScoped<IModelStateCreator, DefaultModelStateCreator>();
        services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    
    // Configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
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