using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Entities;
using CrmBackend.Infrastructure.Data; // ⬅️ ضروري نوصل للـ DbContext
using Microsoft.EntityFrameworkCore;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public AuthenticationService(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public string GenerateToken(User user)
    {
        // ✅ جلب صلاحيات المستخدم من جدول RoleClaims
        var roleClaims = _context.RoleClaims
            .Where(rc => rc.RoleId == user.RoleId && rc.Type == "Permission")
            .Select(rc => rc.Value)
            .ToList();

        // ✅ بناء claims الأساسية
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role?.Name ?? "User"),
            new Claim("BranchId", user.BranchId.ToString())
        };

        // ✅ إضافة الصلاحيات كـ Claims
        claims.AddRange(roleClaims.Select(p => new Claim("Permission", p)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
