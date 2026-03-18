namespace MBudziszSolution.Models;

public class Project
{
    public required string Id { get; set; }
    public required string OrgId { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; }
    public required int BudgetHours { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public required string Description { get; set; }
}
