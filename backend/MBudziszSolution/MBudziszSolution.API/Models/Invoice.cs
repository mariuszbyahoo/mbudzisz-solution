namespace MBudziszSolution.Models;

public class Invoice
{
    public required string Id { get; set; }
    public required string OrgId { get; set; }
    public required string ProjectId { get; set; }
    public required decimal Amount { get; set; }
    public required string Currency { get; set; }
    public required string Status { get; set; }
    public string? DueDate { get; set; }
    public string? IssuedAt { get; set; }
    public required string Description { get; set; }
}
