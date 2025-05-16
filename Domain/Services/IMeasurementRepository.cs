using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface IMeasurementRepository
{
    Task AddAsync(Measurement measurement);
    Task<Measurement?> GetByTaskIdAsync(int taskId);
    Task UpdateAsync(Measurement measurement);

}

