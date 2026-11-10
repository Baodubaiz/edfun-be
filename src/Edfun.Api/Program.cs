using Edfun.Api.Common.Auth;
using Edfun.Api.Common.Data;
using Edfun.Api.Common.Modules;
using Edfun.Api.Modules.Users;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddJwtAuthentication(builder.Configuration);

// Register every module here (like `imports: [...]` in a NestJS AppModule).
builder.Services.AddModules(builder.Configuration,
    new UsersModule());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
