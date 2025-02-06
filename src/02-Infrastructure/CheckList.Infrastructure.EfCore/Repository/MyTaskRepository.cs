using CheckList.Domain.Core.Contracts.Repository;
using CheckList.Domain.Core.Dtos;
using CheckList.Domain.Core.Entities;
using CheckList.Infrastructure.EfCore.Commen;
using Microsoft.EntityFrameworkCore;

namespace CheckList.Infrastructure.EfCore.Repository;

public class MyTaskRepository(CheckListDbContext context) : IMyTaskRepository
{
    public async Task<int> GetIncompleteTaskCount(int userId, CancellationToken cancellationToken)
    {
        var result = await context.MyTasks.AsNoTracking()
             .Where(t => t.UserId == userId && !t.IsCompleted)
             .CountAsync(cancellationToken);
        return result;
    }

    public async Task Create(MyTaskDto task, int userId, CancellationToken cancellationToken)
    {

        MyTask item = new()
        {
            UserId = userId,
            Title = task.Title,
            IsCompleted = false,
            TimeToDone = task.TimeToDone,

        };
        await context.MyTasks.AddAsync(item, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var item = await context.MyTasks.FindAsync([id], cancellationToken) ?? throw new KeyNotFoundException("Task Not Found");
        context.MyTasks.Remove(item);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<MyTaskDto>> GetAll(int userId, CancellationToken cancellationToken)
    {
        var items = await context.MyTasks.AsNoTracking()
              .Where(t => t.UserId == userId)
              .Select(t => new MyTaskDto
              {
                  Id = t.Id,
                  Title = t.Title,
                  IsCompleted = t.IsCompleted,
                  TimeToDone = t.TimeToDone,


              }).ToListAsync(cancellationToken);
        return items;


    }

    public async Task MarkAsCompleted(int id, CancellationToken cancellationToken)
    {
        var item = await context.MyTasks.FindAsync([id], cancellationToken) ?? throw new KeyNotFoundException("Task Not Found");
        item.IsCompleted = true;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(MyTaskDto model, CancellationToken cancellationToken)
    {

        var item = await context.MyTasks.FindAsync([model.Id], cancellationToken) ?? throw new KeyNotFoundException("Task Not Found");
        item.Title = model.Title;
        item.IsCompleted = model.IsCompleted;
        await context.SaveChangesAsync(cancellationToken);

    }

    public async Task<List<MyTaskDto>> GetAllIncompleted(int userId, CancellationToken cancellationToken)
    {
        var items = await context.MyTasks.AsNoTracking()
             .Where(t => t.UserId == userId && !t.IsCompleted)
             .Select(t => new MyTaskDto
             {
                 Id = t.Id,
                 Title = t.Title,
                 IsCompleted = t.IsCompleted,
                 TimeToDone = t.TimeToDone

             })
             .ToListAsync(cancellationToken);
        return items;
    }

    public async Task<List<MyTaskDto>> GetAllByDate(DateTime timeToDone, int userId, CancellationToken cancellationToken)
    {
        var items = await context.MyTasks.AsNoTracking()
             .Where(t => t.UserId == userId && t.TimeToDone == timeToDone)
             .Select(t => new MyTaskDto
             {
                 Id = t.Id,
                 Title = t.Title,
                 IsCompleted = t.IsCompleted,
                 TimeToDone = t.TimeToDone

             })
             .ToListAsync(cancellationToken);
        return items;

    }

    public async Task<MyTaskDto> Get(int id, CancellationToken cancellationToken)
    {
        var item = await context.MyTasks.AsNoTracking()
             .Where(t => t.Id == id)
             .Select(t => new MyTaskDto
             {

                 Id = t.Id,
                 Title = t.Title,
                 IsCompleted = t.IsCompleted,
                 TimeToDone = t.TimeToDone
             }).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Task Not Found");
        return item;
    }
}
