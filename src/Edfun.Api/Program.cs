using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

// Register modules here (Auth, Users, Catalog, Categories, Administration) once they have code.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
