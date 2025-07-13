using LionPetManagement_BLL;
using LionPetManagement_HoaLQ.Models;
using Microsoft.AspNetCore.SignalR;

namespace LionPetManagement_HoaLQ.Hub;

public class LionProfileHub: Microsoft.AspNetCore.SignalR.Hub
{
    private readonly LionProfileService _lionProfileService;
    public LionProfileHub(LionProfileService lionProfileService)
    {
        _lionProfileService = lionProfileService;
    }
    public async Task Hub_DeleteProfileAsync(string lionProfileId)
    {
        await Clients.All.SendAsync("ReceiveDeleteProfile", lionProfileId);
        await _lionProfileService.DeleteProfileAsync(int.Parse(lionProfileId));
    }
}