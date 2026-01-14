namespace AkiRange.Api.Data.Entities;

public class TaskEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public List<PlannedItemEntity> PlannedItems { get; set; } = new();
    public List<CommitMappingEntity> CommitMappings { get; set; } = new();
}
