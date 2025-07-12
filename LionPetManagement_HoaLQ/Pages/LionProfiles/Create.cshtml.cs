using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LionPetManagement_HoaLQ.Models;

namespace LionPetManagement_HoaLQ.Pages.LionProfiles
{
    public class CreateModel : PageModel
    {
        private readonly LionPetManagement_HoaLQ.Models.SU25LionDBContext _context;

        public CreateModel(LionPetManagement_HoaLQ.Models.SU25LionDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LionTypeId"] = new SelectList(_context.LionTypes, "LionTypeId", "LionTypeId");
            return Page();
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LionProfiles.Add(LionProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
