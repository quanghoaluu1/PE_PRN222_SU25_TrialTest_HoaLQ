using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_HoaLQ.Models;

namespace LionPetManagement_HoaLQ.Pages.LionProfiles
{
    public class DeleteModel : PageModel
    {
        private readonly LionPetManagement_HoaLQ.Models.SU25LionDBContext _context;

        public DeleteModel(LionPetManagement_HoaLQ.Models.SU25LionDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _context.LionProfiles.FirstOrDefaultAsync(m => m.LionProfileId == id);

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

            var lionprofile = await _context.LionProfiles.FindAsync(id);
            if (lionprofile != null)
            {
                LionProfile = lionprofile;
                _context.LionProfiles.Remove(LionProfile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
