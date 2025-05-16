using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Infrastructure.Repositories;

public class InquiryTaskRepository : IInquiryTaskRepository
{
    private readonly ApplicationDbContext _context;

    public InquiryTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(InquiryTask task)
    {
        try
        {
            _context.InquiryTasks.Add(task);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error: " + ex.Message);
            throw;
        }

    }

    public async Task<InquiryTask?> GetMeasurementTaskByInquiryAndUserAsync(int inquiryId, Guid userId)
    {
        return await _context.InquiryTasks
            .Include(t => t.Inquiry) // مهم حتى نقدر نعدّل حالة الاستفسار
            .FirstOrDefaultAsync(t =>
                t.InquiryId == inquiryId &&
                t.IsActive && !t.IsDeleted &&
                t.AssignedToUserId == userId &&
                t.TaskType == TaskType.Measurement);
    }
    public async Task<InquiryTask?> GetByInquiryIdAsync(int inquiryId)
    {
        return await _context.InquiryTasks
            .FirstOrDefaultAsync(t => t.InquiryId == inquiryId && !t.IsDeleted);
    }

    public async Task UpdateAsync(InquiryTask task)
    {
        _context.InquiryTasks.Update(task);
        await _context.SaveChangesAsync();
    }
}

