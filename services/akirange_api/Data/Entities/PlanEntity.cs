namespace AkiRange.Api.Data.Entities;

public class PlanEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime WindowStartUtc { get; set; }
    public DateTime WindowEndUtc { get; set; }
    public DateTime GeneratedAtUtc { get; set; } = DateTime.UtcNow;

    public List<PlannedItemEntity> Items { get; set; } = new();
    public List<CommitMappingEntity> CommitMappings { get; set; } = new();
}
