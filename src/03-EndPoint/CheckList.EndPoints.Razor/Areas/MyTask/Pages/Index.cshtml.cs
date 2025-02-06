
using CheckList.Domain.Core.Contracts.AppService;
using CheckList.Domain.Core.Dtos;
using CheckList.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CheckList.EndPoints.Razor.Areas.MyTask.Pages
{
    [Authorize]
    public class IndexModel(UserManager<User> userManager, IMyTaskAppService myTaskAppService) : PageModel
    {
        [BindProperty]
        public MyTaskDto Task { get; set; } = new();
        [BindProperty]
        public List<MyTaskDto> Tasks { get; set; } = [];


        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id) || id <= 0)
            {
                return RedirectToPage("/Account/Login", new { Area = "Identity" });
            }
            Tasks = await myTaskAppService.GetAll(id, cancellationToken);
            return Page();
        }
        public async Task<IActionResult> OnPostCreateTask(CancellationToken cancellationToken)
        {
            var userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id) || id <= 0)
            {
                return RedirectToPage("/Account/Login", new { Area = "Identity" });
            }
            if (ModelState.IsValid)
            {
              
                    var result = await myTaskAppService.Create(Task, id, cancellationToken);
                    TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;

                return RedirectToPage();
            }
            return Page();

        }

        public async Task<IActionResult> OnPostCompleteTask(int id, CancellationToken cancellationToken)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userRef) || userRef <= 0)

            {
                return RedirectToPage("/Account/Login", new { Area = "Identity" });
            }
            var result = await myTaskAppService.MarkAsCompleted(id, cancellationToken);
            TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;
            return RedirectToPage();
        }


    }
}
