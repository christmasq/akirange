namespace AkiRange.Api.Data.Entities;

public class PlannedItemEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PlanId { get; set; }
    public PlanEntity? Plan { get; set; }

    public Guid TaskId { get; set; }
    public TaskEntity? Task { get; set; }

    public string TitleSnapshot { get; set; } = string.Empty;
    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }
}
