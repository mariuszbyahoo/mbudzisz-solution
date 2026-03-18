namespace MBudziszSolution.Models;

public class User
{
    public required string Id { get; set; }
    public required string OrgId { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
    public required bool Active { get; set; }
    public required string Bio { get; set; }
}
