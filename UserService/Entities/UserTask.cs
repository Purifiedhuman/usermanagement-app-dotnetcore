namespace UserService.Entities;

public class UserTask : BaseEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; } = false;
    public ApplicationUser? User { get; set; }
}
