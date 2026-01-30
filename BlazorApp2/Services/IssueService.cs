using YourApp.Models;

namespace YourApp.Services;

public sealed class IssueService
{
    private readonly List<Issue> _issues = new();
    private int _nextId = 1;

    public IssueService()
    {
        _issues.Add(new Issue
        {
            Id = _nextId++,
            Title = "Första ärendet",
            Description = "Seed-data för att testa listan.",
            Status = IssueStatus.Open,
            CreatedUtc = DateTime.UtcNow
        });
    }

    public IReadOnlyList<Issue> GetAll() => _issues;

    public Issue? GetById(int id) => _issues.FirstOrDefault(i => i.Id == id);

    public Issue Add(string title, string description)
    {
        var issue = new Issue
        {
            Id = _nextId++,
            Title = title,
            Description = description,
            Status = IssueStatus.Open,
            CreatedUtc = DateTime.UtcNow
        };

        _issues.Add(issue);
        return issue;
    }

    public bool UpdateStatus(int id, IssueStatus status)
    {
        var issue = GetById(id);
        if (issue is null) return false;

        issue.Status = status;
        return true;
    }
}
