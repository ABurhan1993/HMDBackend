using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Infrastructure.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly ApplicationDbContext _context;

    public MeasurementRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Measurement measurement)
    {
        _context.Measurements.Add(measurement);
        await _context.SaveChangesAsync();
    }

    public async Task<Measurement?> GetByTaskIdAsync(int taskId)
    {
        return await _context.Measurements
            .FirstOrDefaultAsync(m => m.InquiryTaskId == taskId && !m.IsDeleted);
    }



    public async Task UpdateAsync(Measurement measurement)
    {
        _context.Measurements.Update(measurement);
        await _context.SaveChangesAsync();
    }

}

