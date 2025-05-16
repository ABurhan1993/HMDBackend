using CrmBackend.Domain.Entities;

namespace CrmBackend.Domain.Services;

public interface ITaskFileRepository
{
    Task<List<TaskFile>> GetByTaskIdAsync(int taskId);
    Task UpdateAsync(TaskFile file);
}

