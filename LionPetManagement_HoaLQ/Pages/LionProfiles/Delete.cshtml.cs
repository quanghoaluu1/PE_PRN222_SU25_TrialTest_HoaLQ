using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LionPetManagement_BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_HoaLQ.Models;
using Microsoft.AspNetCore.Authorization;

namespace LionPetManagement_HoaLQ.Pages.LionProfiles
{
    [Authorize(Roles = "2")]

    public class DeleteModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;

        public DeleteModel(LionProfileService lionProfileService)
        {
            _lionProfileService = lionProfileService;
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _lionProfileService.GetProfileByIdAsync(id.Value);

            if (lionprofile == null)
            {
                return NotFound();
            }
            else
            {
                LionProfile = lionprofile;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _lionProfileService.GetProfileByIdAsync(id.Value);
            if (lionprofile != null)
            {
                LionProfile = lionprofile;
                await _lionProfileService.DeleteProfileAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
