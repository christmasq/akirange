namespace AkiRange.Api.Data.Entities;

public class PlannedItemEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PlanId { get; set; }
    public PlanEntity? Plan { get; set; }

    public Guid TaskId { get; set; }
    public TaskEntity? Task { get; set; }

    public DateTime StartAtUtc { get; set; }
    public DateTime EndAtUtc { get; set; }
}
