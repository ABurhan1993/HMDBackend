using CrmBackend.Domain.Services;
using CrmBackend.Domain.Entities;
using CrmBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CrmBackend.Domain.Enums;
using CrmBackend.Application.DTOs.CustomersDTOs;

namespace CrmBackend.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer.CustomerId;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Branch)
            .Include(c => c.CustomerAssignedToUser)
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.Where(c => c.IsActive == true && c.IsDeleted == false).ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Customer>> GetByContactStatusAsync(ContactStatus status)
    {
        return await _context.Customers
            .Where(c => c.ContactStatus == status && !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> GetByWayOfContactAsync(WayOfContact way)
    {
        return await _context.Customers
            .Where(c => c.WayOfContact == way && !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> GetByAssignedToIdAsync(Guid userId)
    {
        return await _context.Customers
            .Where(c => c.CustomerAssignedTo == userId && !c.IsDeleted)
            .ToListAsync();
    }
    public IQueryable<Customer> Query()
    {
        return _context.Customers.AsQueryable();
    }
    public async Task SoftDeleteAsync(int customerId, Guid deletedBy)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null) return;

        customer.IsDeleted = true;
        customer.UpdatedBy = deletedBy;
        customer.UpdatedDate = DateTime.UtcNow;

        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Customer>> GetUpcomingMeetingsAsync()
    {
        return await _context.Customers
            .Include(c => c.CustomerAssignedToUser)
            .Where(c => c.CustomerNextMeetingDate != null && c.IsActive && !c.IsDeleted)
            .ToListAsync();
    }

    // CustomerRepository.cs
    public async Task<Customer?> FindByPhoneAsync(string phone)
    {
        return await _context.Customers
            .Include(c => c.Branch)
            .Include(c => c.CustomerAssignedToUser)
            .FirstOrDefaultAsync(c => c.CustomerContact == phone && c.IsActive && c.IsDeleted);
    }

    public async Task<List<CustomerCountByUserDto>> GetCountGroupedByCreatedByAsync(int branchId)
    {
        return await _context.Customers
            .Where(c => c.BranchId == branchId && c.UserId != null && c.IsActive && !c.IsDeleted)
            .GroupBy(c => new { c.UserId, c.User.FullName })
            .Select(g => new CustomerCountByUserDto
            {
                UserId = g.Key.UserId ?? Guid.Empty,
                UserName = g.Key.FullName,
                Count = g.Count()
            })
            .ToListAsync();
    }

    public async Task<List<CustomerCountByUserDto>> GetCountGroupedByAssignedToAsync(int branchId)
    {
        return await _context.Customers
            .Where(c => c.BranchId == branchId && c.CustomerAssignedTo != null && c.IsActive && !c.IsDeleted)
            .GroupBy(c => new { c.CustomerAssignedTo, c.CustomerAssignedToUser.FullName })
            .Select(g => new CustomerCountByUserDto
            {
                UserId = g.Key.CustomerAssignedTo ?? Guid.Empty,
                UserName = g.Key.FullName,
                Count = g.Count()
            })
            .ToListAsync();
    }




}
