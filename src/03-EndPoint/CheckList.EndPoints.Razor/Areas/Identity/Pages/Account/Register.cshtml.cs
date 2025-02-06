using System.ComponentModel.DataAnnotations;
using CheckList.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CheckList.EndPoints.Razor.Areas.Identity.Pages.Account
{
    public class RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager) : PageModel
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly UserManager<User> _userManager = userManager;


        [BindProperty]
        public InputModel Input { get; set; } = null!;



        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = null!;
            [Required]
            [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = null!;

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = null!;

            [Required(ErrorMessage = "Invalid Name")]
            public string Name { get; set; } = null!;
            public string Family { get; set; } = null!;
        }


        public void OnGetAsync()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {


            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Name = Input.Name,
                    Family = Input.Family,
                    Email = Input.Email,
                    UserName = Input.Email,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var resultLogin = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);
                    if (resultLogin.Succeeded)
                        return RedirectToPage("/Index", new { Area = "MyTask" });
                    else
                        return RedirectToPage("Login");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }



    }
}
