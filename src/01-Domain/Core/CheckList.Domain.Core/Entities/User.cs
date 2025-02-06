using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace CheckList.Domain.Core.Entities;

public class User : IdentityUser<int>
{

    [Required(ErrorMessage = "Invalid Name")]
    public string Name { get; set; } = null!;
    public string? Family { get; set; }
    public List<MyTask> Tasks { get; set; } = [];
}

