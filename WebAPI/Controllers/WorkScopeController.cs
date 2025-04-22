using CrmBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkscopeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkscopeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/workscope/all
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
