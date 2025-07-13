using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LionPetManagement_BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LionPetManagement_HoaLQ.Models;
using Microsoft.AspNetCore.Authorization;

namespace LionPetManagement_HoaLQ.Pages.LionProfiles
{
    [Authorize(Roles = "2")]
    public class CreateModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;
        private readonly LionTypeService _lionTypeService;
        public CreateModel(LionProfileService lionProfileService, LionTypeService lionTypeService)
        {
            _lionProfileService = lionProfileService;
            _lionTypeService = lionTypeService;
        }

        public IActionResult OnGet()
        {
            var lionTypes = _lionTypeService.GetAllLionTypesAsync();
        ViewData["LionTypeId"] = new SelectList(lionTypes, "LionTypeId", "LionTypeName");
            return Page();
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;
        
        public string ErrorMessage { get; set; } = string.Empty;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ErrorMessage = _lionProfileService.CheckRegexProfileName(LionProfile.LionName);
            await _lionProfileService.CreateProfileAsync(LionProfile);

            return RedirectToPage("./Index");
        }
    }
}
