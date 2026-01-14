namespace AkiRange.Api.Data.Entities;

public class PlanEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime StartAtUtc { get; set; }
    public DateTime EndAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public List<PlannedItemEntity> Items { get; set; } = new();
    public List<CommitMappingEntity> CommitMappings { get; set; } = new();
}
