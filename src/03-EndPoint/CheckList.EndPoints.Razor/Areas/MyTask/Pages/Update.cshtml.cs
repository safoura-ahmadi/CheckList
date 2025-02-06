using CheckList.Domain.Core.Contracts.AppService;
using CheckList.Domain.Core.Dtos;
using CheckList.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CheckList.EndPoints.Razor.Areas.MyTask.Pages
{
    public class UpdateModel(UserManager<User> userManager, IMyTaskAppService myTaskAppService) : PageModel
    {
        [BindProperty]
        public MyTaskDto Task { get; set; } = new();
        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out _) || id <= 0)
            {
                return RedirectToPage("/Account/Login", new { Area = "Identity" });
            }
            var task = await myTaskAppService.Get(id, cancellationToken);
            if (task == null)
            {
                TempData["ErrorMessage"] = "Task Not Fpund";
                return RedirectToPage("Index");
            }
            Task = task;
            return Page();


        }
        public async Task<IActionResult> OnPostUpdateTask(CancellationToken cancellationToken)
        {
            var userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userRef) || userRef <= 0)
            {
                return RedirectToPage("/Account/Login", new { Area = "Identity" });
            }
            if (ModelState.IsValid)
            {
                var result = await myTaskAppService.Update(Task, cancellationToken);
                TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
                return RedirectToPage("Index");
            }
            return Page();
        }

    }
}
