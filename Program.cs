using CrmBackend.Application.Handlers;
using CrmBackend.Application.Interfaces;
using CrmBackend.Application.Services;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CrmBackend.Infrastructure.Data;
using CrmBackend.Domain.Constants;
using CrmBackend.Application.Handlers.CustomerHandlers;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173", "https://mhdcrm.onrender.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanCreateOrUpdate", policy =>
        policy.RequireRole(RoleConstants.Admin, RoleConstants.User));

    options.AddPolicy("CanComment", policy =>
        policy.RequireRole(RoleConstants.Admin, RoleConstants.User));

    options.AddPolicy("CanView", policy =>
        policy.RequireRole(RoleConstants.Admin, RoleConstants.User, RoleConstants.Designer));

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(RoleConstants.Admin));
});

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerCommentRepository, CustomerCommentRepository>();

// Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Handlers
builder.Services.AddScoped<LoginCommandHandler>();
builder.Services.AddScoped<RegisterAdminCommandHandler>();

builder.Services.AddScoped<CreateCustomerCommandHandler>();
builder.Services.AddScoped<UpdateCustomerHandler>();
builder.Services.AddScoped<UpdateCustomerAssignmentHandler>();
builder.Services.AddScoped<AddCustomerCommentHandler>();

builder.Services.AddScoped<GetAllCustomersHandler>();
builder.Services.AddScoped<GetCustomerByIdHandler>();
builder.Services.AddScoped<GetCustomersByContactStatusHandler>();
builder.Services.AddScoped<GetCustomersByWayOfContactHandler>();
builder.Services.AddScoped<GetCustomersByAssignedToIdHandler>();

// MVC Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Middleware
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Swagger UI
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
