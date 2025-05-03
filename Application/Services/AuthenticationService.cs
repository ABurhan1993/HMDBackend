using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Entities;
using CrmBackend.Infrastructure.Data;
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

    public string GenerateToken(User user, List<string> permissions)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim("FirstName", user.FirstName),
        new Claim("LastName", user.LastName ?? ""),
        new Claim("Name", user.FullName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("Phone", user.Phone),
        new Claim("UserImageUrl", user.UserImageUrl ?? ""),
        new Claim("IsNotificationEnabled", user.IsNotificationEnabled.ToString()),
        new Claim(ClaimTypes.Role, user.Role?.Name ?? "User"),
        new Claim("BranchId", user.BranchId.ToString())
    };

        // ✅ إضافة الصلاحيات بشكل صحيح
        if (permissions != null && permissions.Any())
        {
            foreach (var permission in permissions)
            {
                claims.Add(new Claim("permission", permission));
            }
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

}

