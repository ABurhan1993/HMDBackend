using CrmBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrmBackend.Domain.Constants;

namespace CrmBackend.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkscopeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkscopeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = PermissionConstants.Inquiries.View)] // 🔥 صلاحية مشاهدة الأعمال المرتبطة بالاستفسارات
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var workscopes = await _context.WorkScopes
                .Select(w => new
                {
                    w.WorkScopeId,
                    w.WorkScopeName
                })
                .ToListAsync();

            return Ok(workscopes);
        }
    }
}
