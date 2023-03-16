using System.Reflection;
using CleanApi.Entities;
using CleanApi.PipelineBehaviors;
using CleanApi.Repositories;
using CleanApi.Repositories.Abstract;
using CleanApi.Strategies;
using CleanApi.Strategies.Abstract;
using FluentValidation;
using MediatR;

namespace CleanApi;

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
        services.AddSingleton<IRepository<ProductEntity>, InMemoryProductRepository>();
        services.AddSingleton<IRepository<CustomerEntity>,InMemoryCustomerRepository>();
        services.AddSingleton<IRepository<OrderEntity>, InMemoryOrderRepository>();
        
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
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