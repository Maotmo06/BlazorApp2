using System.ComponentModel.DataAnnotations;

namespace YourApp.Dtos;

public sealed class CreateIssueDto
{
    [Required]
    [StringLength(60)]
    public string Title { get; set; } = "";

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = "";
}
