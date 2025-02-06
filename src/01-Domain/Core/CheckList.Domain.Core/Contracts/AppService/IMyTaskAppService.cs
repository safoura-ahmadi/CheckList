using CheckList.Domain.Core.Dtos;
using CheckList.Domain.Core.Entities;

namespace CheckList.Domain.Core.Contracts.AppService;

public interface IMyTaskAppService
{
    Task<Result> Create(MyTaskDto task, int userId, CancellationToken cancellationToken);
    Task<List<MyTaskDto>> GetAll(int userId, CancellationToken cancellationToken);
    Task<List<MyTaskDto>> GetAllIncompleted(int userId, CancellationToken cancellationToken);
    Task<List<MyTaskDto>> GetAllByDate(DateTime timeToDone, int userId, CancellationToken cancellationToken);
    Task<int> GetIncompleteTaskCount(int userId, CancellationToken cancellationToken);
    Task<Result> Update(MyTaskDto model,CancellationToken cancellationToken);
    Task<Result> MarkAsCompleted(int id, CancellationToken cancellationToken);
    Task<Result> Delete(int id, CancellationToken cancellationToken);
}
