using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LionPetManagement_BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_HoaLQ.Models;

namespace LionPetManagement_HoaLQ.Pages.LionProfiles
{
    public class IndexModel : PageModel
    {
        private readonly LionPetManagement_HoaLQ.Models.SU25LionDBContext _context;
        private readonly LionProfileService _lionProfileService;

        public IndexModel(LionProfileService lionProfileService)
        {
            _lionProfileService = lionProfileService;
        }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public IList<LionProfile> LionProfile { get;set; } = default!;

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 3)
        {
            var totalCount = await _lionProfileService.GetTotalProfileCountAsync();
            PageIndex = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            LionProfile = await _lionProfileService.GetAllProfilesAsync(PageIndex, pageSize);
        }
    }
}
