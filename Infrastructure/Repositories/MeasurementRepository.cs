using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Data;

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
}

