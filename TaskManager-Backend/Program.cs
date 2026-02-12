using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using TaskManager.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- 1. (Service Configuration) ---.

// Configure CORS to allow the front-end (Angular) to communicate with the API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Setting up DbContext with SQL Server (make sure the connection string in appsettings.json is updated).
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registering the repository for dependency injection.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// JWT Authentication setup.
var jwtKey = builder.Configuration["Jwt:Key"] ?? "SecretKey_At_Least_32_Chars_2026_Key";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = "TaskManager",
            ValidateAudience = true,
            ValidAudience = "TaskUser",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // This ensures that tokens expire exactly at their expiration time (no additional grace period).
        };
    });

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddOpenApi();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TaskManager.Validator.TaskValidator>();

var app = builder.Build();

// --- 2. (Middleware Pipeline) ---.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Enforce HTTPS for secure communication.
app.UseHttpsRedirection();

app.UseCors("AllowAngular"); // Enable CORS to allow the front-end (Angular) to communicate with the API.

app.UseAuthentication();    // This middleware checks for the presence of a valid JWT token in the incoming requests and sets the user context accordingly.
app.UseAuthorization();     // This ensures that only authenticated users can access the API endpoints, and it will enforce any authorization policies defined in the controllers.

app.MapControllers();

app.Run();
