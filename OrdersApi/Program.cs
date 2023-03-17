using OrdersApi;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
startup.Configure(builder.Build(), builder.Environment);