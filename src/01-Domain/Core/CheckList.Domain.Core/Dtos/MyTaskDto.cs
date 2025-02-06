using CheckList.Domain.Core.Entities.ValidationAtrribute;
using System.ComponentModel.DataAnnotations;

namespace CheckList.Domain.Core.Dtos;

public class MyTaskDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Invalid Title")]
    public string Title { get; set; } = null!;
    public bool IsCompleted { get; set; }
    [Required(ErrorMessage ="Determine Date")]
    [DateValidation(ErrorMessage = "Invalid Date")]
    public DateTime TimeToDone { get; set; }
}
