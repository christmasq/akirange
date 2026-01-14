namespace AkiRange.Api.Data.Entities;

public class CommitMappingEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PlanId { get; set; }
    public PlanEntity? Plan { get; set; }

    public Guid TaskId { get; set; }
    public TaskEntity? Task { get; set; }

    public string? GoogleEventId { get; set; }
    public DateTime? CommittedAtUtc { get; set; }
}
