namespace AkiRange.Api.Data.Entities;

public class TaskEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public int OccurrencesPerWeek { get; set; } = 1;
    public TimeOnly? EarliestStartLocalTime { get; set; }
    public TimeOnly? LatestEndLocalTime { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public List<PlannedItemEntity> PlannedItems { get; set; } = new();
    public List<CommitMappingEntity> CommitMappings { get; set; } = new();
}
