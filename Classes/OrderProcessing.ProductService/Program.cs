using OrderProcessing.ProductService.AppDbContext;
using OrderProcessing.ProductService.Application.Implementations;
using OrderProcessing.ProductService.Application.Interfaces;
using OrderProcessing.ProductService.Domain.Common;
using OrderProcessing.ProductService.Infrastructure;
using OrderProcessing.ProductService.Middleware;
using Serilog;

try
{
    // Create the logger early to catch startup issues
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateBootstrapLogger();
        
    Log.Information("Starting up ProductService");

    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog from appsettings.json
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    builder.Services.AddLogging();
    
    builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
    builder.Services.AddSingleton<ProductDbContext>();

        // Register repositories and services
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductService, ProductService>();
    
    
    var app = builder.Build();

    // { 
    //     using var scope = app.Services.CreateScope();
    //     var context = scope.ServiceProvider;
    // }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseExceptionHandler();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}