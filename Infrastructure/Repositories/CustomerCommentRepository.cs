using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Infrastructure.Repositories
{
    public class CustomerCommentRepository : ICustomerCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CustomerComment comment)
        {
            _context.CustomerComments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CustomerComment>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.CustomerComments.Include(c => c.CommentAddedByNavigation)
                .Where(c => c.CustomerId == customerId && !c.IsDeleted)
                .OrderByDescending(c => c.CommentAddedOn)
                .ToListAsync();
        }


    }
}
