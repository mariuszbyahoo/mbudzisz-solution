namespace MBudziszSolution.Models;

public class TimeEntry
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public required string ProjectId { get; set; }
    public required string Date { get; set; }
    public required int Hours { get; set; }
    public required string Description { get; set; }
}
