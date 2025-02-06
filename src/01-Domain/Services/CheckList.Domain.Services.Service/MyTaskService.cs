using CheckList.Domain.Core.Contracts.Repository;
using CheckList.Domain.Core.Contracts.Service;
using CheckList.Domain.Core.Dtos;
using CheckList.Domain.Core.Entities;
using Microsoft.VisualBasic;

namespace CheckList.Domain.Services.Service;

public class MyTaskService(IMyTaskRepository repository) : IMyTaskService
{
    public async Task Create(MyTaskDto task, int userId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId);
        await repository.Create(task, userId, cancellationToken);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        await repository.Delete(id, cancellationToken);
    }

    public async Task<MyTaskDto> Get(int id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        return await repository.Get(id, cancellationToken);
    }

    public async Task<List<MyTaskDto>> GetAll(int userId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId);
        return await repository.GetAll(userId, cancellationToken);
    }

    public async Task<List<MyTaskDto>> GetAllByDate(DateTime timeToDone, int userId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId);
        return await repository.GetAllByDate(timeToDone, userId, cancellationToken);
    }

    public async Task<List<MyTaskDto>> GetAllIncompleted(int userId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId);
        return await repository.GetAllIncompleted(userId, cancellationToken);
    }

    public async Task<int> GetIncompleteTaskCount(int userId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId);
        return await repository.GetIncompleteTaskCount(userId, cancellationToken);
    }

    public async Task MarkAsCompleted(int id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        await repository.MarkAsCompleted(id, cancellationToken);
    }

    public async Task Update(MyTaskDto model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);
        await repository.Update(model, cancellationToken);
    }
}
