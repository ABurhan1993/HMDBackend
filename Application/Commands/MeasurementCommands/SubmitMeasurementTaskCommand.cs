using System.ComponentModel.DataAnnotations;

namespace CrmBackend.Application.Commands.MeasurementCommands;

public class SubmitMeasurementTaskCommand
{
    [Required]
    public int InquiryId { get; set; }

    public string? Comment { get; set; }

    [Required]
    public List<IFormFile> Files { get; set; } = new();
}
