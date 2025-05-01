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
using CrmBackend.Infrastructure.Seeding;
using CrmBackend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CrmBackend.Infrastructure.Services;

// Handlers
using CrmBackend.Application.Handlers.CustomerHandlers;
using CrmBackend.Application.Handlers.UserHandlers;
using CrmBackend.Application.Handlers.BranchHandlers;
using CrmBackend.Application.Handlers.RoleHandlers;
using CrmBackend.Application.Handlers.InquiryHandlers;
using CrmBackend.Application.CustomerHandlers;
using CrmBackend.Application.Handlers.UserClaimHandlers;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",
            "http://localhost:5173",
            "https://mhdcrm.onrender.com",
            "https://www.hmdserver.com",
            "https://hmdserver.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    var permissions = PermissionConstants.All;
    foreach (var permission in permissions)
    {
        options.AddPolicy(permission, policy =>
            policy.RequireClaim("Permission", permission));
    }

    options.AddPolicy("UsersOrCustomersView", policy =>
        policy.Requirements.Add(new PermissionOrRequirement(
            PermissionConstants.Users.View,
            PermissionConstants.Customers.View
        )));
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
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IInquiryRepository, InquiryRepository>();
builder.Services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
builder.Services.AddScoped<IUserClaimRepository, UserClaimRepository>();

// Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<PermissionHelper>();
builder.Services.AddHttpContextAccessor();

// Handlers
builder.Services.AddScoped<LoginCommandHandler>();
builder.Services.AddScoped<RegisterAdminCommandHandler>();

builder.Services.AddScoped<CreateCustomerCommandHandler>();
builder.Services.AddScoped<UpdateCustomerHandler>();
builder.Services.AddScoped<UpdateCustomerAssignmentHandler>();
builder.Services.AddScoped<AddCustomerCommentHandler>();
builder.Services.AddScoped<DeleteCustomerHandler>();
builder.Services.AddScoped<GetAllCustomersHandler>();
builder.Services.AddScoped<GetCustomerByIdHandler>();
builder.Services.AddScoped<GetCustomersByContactStatusHandler>();
builder.Services.AddScoped<GetCustomersByWayOfContactHandler>();
builder.Services.AddScoped<GetCustomersByAssignedToIdHandler>();
builder.Services.AddScoped<GetCustomersWithUpcomingMeetingsHandler>();
builder.Services.AddScoped<GetCustomerByPhoneHandler>();
builder.Services.AddScoped<GetCustomerCountByCreatedByHandler>();
builder.Services.AddScoped<GetCustomerCountByAssignedToHandler>();

builder.Services.AddScoped<GetAllUsersHandler>();
builder.Services.AddScoped<CreateUserCommandHandler>();
builder.Services.AddScoped<ResetUserPasswordHandler>();
builder.Services.AddScoped<EditUserCommandHandler>();
builder.Services.AddScoped<GetUsersByBranchIdHandler>();

builder.Services.AddScoped<CreateBranchCommandHandler>();
builder.Services.AddScoped<UpdateBranchCommandHandler>();
builder.Services.AddScoped<GetAllBranchesHandler>();

builder.Services.AddScoped<CreateRoleCommandHandler>();
builder.Services.AddScoped<UpdateRoleCommandHandler>();
builder.Services.AddScoped<GetAllRolesHandler>();

builder.Services.AddScoped<AddInquiryCommandHandler>();
builder.Services.AddScoped<GetInquiriesForDisplayHandler>();
// 🔐 User Claims Handlers
builder.Services.AddScoped<AddUserClaimHandler>();
builder.Services.AddScoped<DeleteUserClaimHandler>();


builder.Services.AddSingleton<IAuthorizationHandler, PermissionOrHandler>();

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Middleware
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

// Swagger UI
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
