using CrmBackend.Application.Handlers;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CrmBackend.Infrastructure.Data;
using CrmBackend.Domain.Constants;
using CrmBackend.Application.Handlers.CustomerHandlers;
using CrmBackend.Infrastructure.Seeding;

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
    // Customers
    options.AddPolicy(PermissionConstants.Customers.View, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Customers.View));
    options.AddPolicy(PermissionConstants.Customers.Create, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Customers.Create));
    options.AddPolicy(PermissionConstants.Customers.Edit, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Customers.Edit));
    options.AddPolicy(PermissionConstants.Customers.Delete, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Customers.Delete));

    // Users
    options.AddPolicy(PermissionConstants.Users.View, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Users.View));
    options.AddPolicy(PermissionConstants.Users.Create, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Users.Create));
    options.AddPolicy(PermissionConstants.Users.Edit, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Users.Edit));
    options.AddPolicy(PermissionConstants.Users.Delete, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Users.Delete));

    // Branches
    options.AddPolicy(PermissionConstants.Branches.View, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Branches.View));
    options.AddPolicy(PermissionConstants.Branches.Create, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Branches.Create));
    options.AddPolicy(PermissionConstants.Branches.Edit, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Branches.Edit));
    options.AddPolicy(PermissionConstants.Branches.Delete, policy =>
        policy.RequireClaim("Permission", PermissionConstants.Branches.Delete));

    // CustomerComments
    options.AddPolicy(PermissionConstants.CustomerComments.View, policy =>
        policy.RequireClaim("Permission", PermissionConstants.CustomerComments.View));
    options.AddPolicy(PermissionConstants.CustomerComments.Create, policy =>
        policy.RequireClaim("Permission", PermissionConstants.CustomerComments.Create));
    options.AddPolicy(PermissionConstants.CustomerComments.Edit, policy =>
        policy.RequireClaim("Permission", PermissionConstants.CustomerComments.Edit));
    options.AddPolicy(PermissionConstants.CustomerComments.Delete, policy =>
        policy.RequireClaim("Permission", PermissionConstants.CustomerComments.Delete));
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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}
app.MapControllers();

// Swagger UI
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

app.Run();
