var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

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
