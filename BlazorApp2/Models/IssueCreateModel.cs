using System.ComponentModel.DataAnnotations;

namespace YourApp.Models;

public sealed class IssueCreateModel
{
    [Required(ErrorMessage = "Titel krävs.")]
    [StringLength(60, ErrorMessage = "Max 60 tecken.")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Beskrivning krävs.")]
    [StringLength(500, ErrorMessage = "Max 500 tecken.")]
    public string Description { get; set; } = "";
}