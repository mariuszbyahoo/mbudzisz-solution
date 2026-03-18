using MBudziszSolution.Data;
using MBudziszSolution.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton(new AggregationService(
    SeedData.Organizations,
    SeedData.Users,
    SeedData.Projects,
    SeedData.TimeEntries,
    SeedData.Invoices
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Json(new
{
    status = "ok",
    message = "Backend running with mock data loaded"
}));

app.MapControllers();

app.Run();

public partial class Program { }
