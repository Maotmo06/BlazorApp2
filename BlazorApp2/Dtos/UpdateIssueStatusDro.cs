using System.ComponentModel.DataAnnotations;
using YourApp.Models;

namespace YourApp.Dtos;

public sealed class UpdateIssueStatusDto
{
    [Required]
    public IssueStatus Status { get; set; }
}
