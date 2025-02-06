using CheckList.Domain.Core.Contracts.AppService;
using CheckList.Domain.Core.Contracts.Service;
using CheckList.Domain.Core.Dtos;
using CheckList.Domain.Core.Entities;
using CheckList.Domain.Core.Entities.Configs;

namespace CheckList.Domian.Services.AppService;

public class MyTaskAppService(IMyTaskService myTaskService,SiteSetting siteSetting) : IMyTaskAppService
{
    public async Task<Result> Create(MyTaskDto task, int userId, CancellationToken cancellationToken)
    {
        var incompletedTaskNo = await myTaskService.GetIncompleteTaskCount(userId,cancellationToken);
        if (siteSetting.Limitation < incompletedTaskNo)
            return Result.Fail("Your Uncompleted Tasks Limit Has Been Reached.");
        try
        {
            
            await myTaskService.Create(task, userId, cancellationToken);
            return Result.Ok("New Task  Created Successfully");
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await myTaskService.Delete(id, cancellationToken);
            return Result.Ok("Selected Task Deleted Successfully");
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<MyTaskDto?> Get(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await myTaskService.Get(id, cancellationToken);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<MyTaskDto>> GetAll(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var items = await myTaskService.GetAll(userId, cancellationToken);
            return items;

        }
        catch
        {
            return [];
        }
    }

    public async Task<List<MyTaskDto>> GetAllByDate(DateTime timeToDone, int userId, CancellationToken cancellationToken)
    {
        return await myTaskService.GetAllByDate(timeToDone, userId, cancellationToken);
    }

    public async Task<List<MyTaskDto>> GetAllIncompleted(int userId, CancellationToken cancellationToken)
    {
        return await myTaskService.GetAllIncompleted(userId, cancellationToken);
    }

    public async Task<int> GetIncompleteTaskCount(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await myTaskService.GetIncompleteTaskCount(userId, cancellationToken);
            return result;
        }
        catch
        {
            return 0;
        }
    }

    public async Task<Result> MarkAsCompleted(int id, CancellationToken cancellationToken)
    {
        try
        {
            await myTaskService.MarkAsCompleted(id, cancellationToken);
            return Result.Ok("Task Done.Congratulations!");
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result> Update(MyTaskDto model, CancellationToken cancellationToken)
    {
        try
        {
            await myTaskService.Update(model, cancellationToken);
            return Result.Ok("Selected Task Edit Successfully");
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
