﻿using CheckList.Domain.Core.Dtos;

namespace CheckList.Domain.Core.Contracts.Repository;

public interface IMyTaskRepository
{
    Task Create(MyTaskDto task, int userId, CancellationToken cancellationToken);
    Task<List<MyTaskDto>> GetAll(int userId, CancellationToken cancellationToken);
    Task<List<MyTaskDto>> GetAllIncompleted(int userId, CancellationToken cancellationToken);
    Task<List<MyTaskDto>> GetAllByDate(DateTime timeToDone,int userId, CancellationToken cancellationToken);
    Task Update(MyTaskDto model, CancellationToken cancellationToken);
    Task MarkAsCompleted(int id, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task<int> GetIncompleteTaskCount(int userId, CancellationToken cancellationToken);
    Task<MyTaskDto> Get(int id, CancellationToken cancellationToken);
}
