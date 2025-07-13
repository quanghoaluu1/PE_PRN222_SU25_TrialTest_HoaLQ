using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using LionPetManagement_BLL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionPetManagement_HoaLQ.Pages.Account;

[AllowAnonymous]

public class LoginModel : PageModel
{
    private readonly LionAccountService _accountService;
    public LoginModel(LionAccountService accountService)
    {
        _accountService = accountService;
    }
    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;


    public string ErrorMessage { get; set; }

    public class InputModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public void OnGet()
    {
        // Nếu có logic khởi tạo
    }

    public async Task<IActionResult> OnPost()
    {
       
        var user = await _accountService.AuthenticateAsync(Email, Password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5)
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties).Wait();
            return RedirectToPage("/Index");
        }

        ErrorMessage = "Sai tên đăng nhập hoặc mật khẩu.";
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Page();
    }
}