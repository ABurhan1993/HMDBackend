using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface IMeasurementRepository
{
    Task AddAsync(Measurement measurement);
}

