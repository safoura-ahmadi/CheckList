using CheckList.Domain.Core.Contracts.AppService;
using CheckList.Domain.Core.Contracts.Service;
using CheckList.Domain.Core.Dtos;
using CheckList.Domain.Core.Entities;

namespace CheckList.Domian.Services.AppService;

public class MyTaskAppService(IMyTaskService myTaskService) : IMyTaskAppService
{
    public async Task<Result> Create(MyTaskDto task, int userId, CancellationToken cancellationToken)
    {
        try
        {
            await myTaskService.Create(task, userId, cancellationToken);
            return Result.Ok("وظیفه ی جدید با موفقیت ایجاد شد");
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
            return Result.Ok("وظیفه ی انتخاب شده با موفقیت حذف شد");
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
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
            return Result.Ok("وضعیت وظیفه در حالت انجام شده قرار گرفت");
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
            return Result.Ok("وظیفه با موفقیت ویرایش شد");
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
