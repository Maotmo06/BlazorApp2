namespace YourApp.Models;

public sealed class Issue
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public IssueStatus Status { get; set; } = IssueStatus.Open;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}