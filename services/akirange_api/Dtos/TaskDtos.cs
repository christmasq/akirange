namespace AkiRange.Api.Dtos;

public sealed class CreateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public int? OccurrencesPerWeek { get; set; }
    public TimeOnly? EarliestStartLocalTime { get; set; }
    public TimeOnly? LatestEndLocalTime { get; set; }
}

public sealed class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public int OccurrencesPerWeek { get; set; }
    public TimeOnly? EarliestStartLocalTime { get; set; }
    public TimeOnly? LatestEndLocalTime { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
