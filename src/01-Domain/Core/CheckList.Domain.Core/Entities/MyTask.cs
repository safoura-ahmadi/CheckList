using CheckList.Domain.Core.Entities.ValidationAtrribute;
using CheckList.Domain.Core.Enums;
using System.ComponentModel.DataAnnotations;
namespace CheckList.Domain.Core.Entities;

public class MyTask
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Invalid Title")]
    public string Title { get; set; } = null!;
    public bool IsCompleted { get; set; }
    [DateValidation(ErrorMessage = "Invalid Date")]
    public DateTime TimeToDone { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}
