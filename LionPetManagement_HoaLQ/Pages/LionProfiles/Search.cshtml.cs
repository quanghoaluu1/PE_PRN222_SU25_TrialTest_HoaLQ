using LionPetManagement_BLL;
using LionPetManagement_HoaLQ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionPetManagement_HoaLQ.Pages.LionProfiles;

public class Search : PageModel
{
    private readonly LionProfileService _lionProfileService;

    public Search(LionProfileService lionProfileService)
    {
        _lionProfileService = lionProfileService;
    }
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    [BindProperty(SupportsGet = true)]
    public double Weight { get; set; } = 0;
    [BindProperty(SupportsGet = true)]
    public string? Name { get; set; } = string.Empty;
    public IList<LionProfile> LionProfile { get;set; } = default!;

    public async Task OnGetAsync(int pageNumber = 1, int pageSize = 3)
    {
        var totalCount = await _lionProfileService.GetTotalProfileCountAsync();
        PageIndex = pageNumber;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        LionProfile = await _lionProfileService.SearchProfilesAsync(Weight, Name, pageNumber, pageSize);
    }
}