using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Services;
using CrmBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CrmBackend.Infrastructure.Repositories;

public class TaskFileRepository : ITaskFileRepository
{
    private readonly ApplicationDbContext _context;

    public TaskFileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskFile>> GetByTaskIdAsync(int taskId)
    {
        return await _context.TaskFiles
            .Where(f => f.InquiryTaskId == taskId && !f.IsDeleted)
            .ToListAsync();
    }

    public async Task UpdateAsync(TaskFile file)
    {
        _context.TaskFiles.Update(file);
        await _context.SaveChangesAsync();
    }
}

