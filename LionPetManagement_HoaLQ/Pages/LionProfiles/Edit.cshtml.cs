using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LionPetManagement_BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_HoaLQ.Models;
using Microsoft.AspNetCore.Authorization;

namespace LionPetManagement_HoaLQ.Pages.LionProfiles
{
    [Authorize(Roles = "2")]

    public class EditModel : PageModel
    {
        private readonly LionPetManagement_HoaLQ.Models.SU25LionDBContext _context;
        private readonly LionTypeService _lionTypeService;
        private readonly LionProfileService _lionProfileService;
        public EditModel(LionTypeService lionTypeService, LionProfileService lionProfileService)
        {
            _lionTypeService = lionTypeService;
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
            var lionTypes =  _lionTypeService.GetAllLionTypesAsync();
            var lionprofile = await _lionProfileService.GetProfileByIdAsync(id.Value);
            if (lionprofile == null)
            {
                return NotFound();
            }
            LionProfile = lionprofile;
            ViewData["LionTypeId"] = new SelectList(lionTypes, "LionTypeId", "LionTypeName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            LionProfile.ModifiedDate = DateTime.Now;
            await _lionProfileService.UpdateProfileAsync(LionProfile);
            return RedirectToPage("./Index");
        }

        private bool LionProfileExists(int id)
        {
            return _context.LionProfiles.Any(e => e.LionProfileId == id);
        }
    }
}
