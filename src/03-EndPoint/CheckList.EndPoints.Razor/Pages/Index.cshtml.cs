using CheckList.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CheckList.EndPoints.Razor.Pages
{
    public class IndexModel(UserManager<User> userManager) : PageModel
    {


        public IActionResult OnGet()
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id) || id <= 0)
            {
                return RedirectToPage("/Account/Login", new { Area = "Identity" });
            }
            return RedirectToPage("/Index", new { Area = "MyTask" });
        }
    }
}
