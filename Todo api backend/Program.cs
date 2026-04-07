using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Todo_api_backend.Config;
using Todo_api_backend.Data;
using Todo_api_backend.Data.Repositories;
using Todo_api_backend.Interfaces.Repositories;
using Todo_api_backend.Interfaces.Services;
using Todo_api_backend.Models;
using Todo_api_backend.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();


// JWT Authentication config and di
var jwtSettings = builder.Configuration
    .GetSection("AuthOptions")
    .Get<JwtSettings>();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("AuthOptions")
);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,

            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,

            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(jwtSettings.Key)
            ),
            ValidateIssuerSigningKey = true,
        };
    });


// Database config and di
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Password hasher di
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Repositories di
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITodoTaskCategoryRepository, TodoTaskCategoryRepository>();

// Services di
builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITodoTaskCategoryService, TodoTaskCategoryService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo service");
});

app.MapControllers();

app.Run();
