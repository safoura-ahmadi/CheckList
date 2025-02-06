using CheckList.Domain.Core.Contracts.AppService;
using CheckList.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CheckList.EndPoints.Razor.Areas.MyTask.Pages
{
    public class DeleteModel(UserManager<User> userManager, IMyTaskAppService myTaskAppService) : PageModel
    {
        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userRef) || userRef <= 0)

            {
                return RedirectToPage("/Account/Login", new { Area = "Identity" });
            }
            var result = await myTaskAppService.Delete(id, cancellationToken);
            TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
            return RedirectToPage("Index");
        }
    }
}
