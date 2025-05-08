using OrderProcessing.InventoryService.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc();

builder.Services.AddControllers();
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

// Configure gRPC endpoints
app.MapGrpcService<InventoryGrpcService>();
app.MapGet("/", () => "Order Service is running");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
