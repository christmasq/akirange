using AkiRange.Api.Data;
using AkiRange.Api.Data.Entities;
using AkiRange.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkiRange.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public TasksController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskDto>>> GetTasks(CancellationToken cancellationToken)
    {
        var tasks = await _dbContext.Tasks
            .AsNoTracking()
            .OrderByDescending(task => task.CreatedAtUtc)
            .Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                DurationMinutes = task.DurationMinutes,
                OccurrencesPerWeek = task.OccurrencesPerWeek,
                EarliestStartLocalTime = task.EarliestStartLocalTime,
                LatestEndLocalTime = task.LatestEndLocalTime,
                CreatedAtUtc = task.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask(
        [FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return BadRequest(new { error = "invalid request body" });
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest(new { error = "title is required" });
        }

        if (request.DurationMinutes <= 0)
        {
            return BadRequest(new { error = "durationMinutes must be > 0" });
        }

        var occurrencesPerWeek = request.OccurrencesPerWeek ?? 1;
        if (occurrencesPerWeek <= 0)
        {
            return BadRequest(new { error = "occurrencesPerWeek must be > 0" });
        }

        var entity = new TaskEntity
        {
            Title = request.Title.Trim(),
            DurationMinutes = request.DurationMinutes,
            OccurrencesPerWeek = occurrencesPerWeek,
            EarliestStartLocalTime = request.EarliestStartLocalTime,
            LatestEndLocalTime = request.LatestEndLocalTime,
            CreatedAtUtc = DateTime.UtcNow
        };

        _dbContext.Tasks.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var response = new TaskDto
        {
            Id = entity.Id,
            Title = entity.Title,
            DurationMinutes = entity.DurationMinutes,
            OccurrencesPerWeek = entity.OccurrencesPerWeek,
            EarliestStartLocalTime = entity.EarliestStartLocalTime,
            LatestEndLocalTime = entity.LatestEndLocalTime,
            CreatedAtUtc = entity.CreatedAtUtc
        };

        return Ok(response);
    }
}
