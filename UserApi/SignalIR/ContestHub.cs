using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace UserApi.SignalIR
{
    [Authorize]
    public class ContestHub : Hub
    {
        private readonly ContestTracker _contestTracker;

        public ContestHub(ContestTracker contestTracker)
        {
            _contestTracker = contestTracker;
        }
        
        public override async Task OnConnectedAsync()
        {
            await _contestTracker.NewContestCreated(new Guid(), DateTime.Now);
            await Clients.Others.SendAsync("New Contest in your area", Context!.User!.FindFirst(ClaimTypes.Name)?.Value);

            var contestResult = _contestTracker.GetNewContests();
            await Clients.All.SendAsync("GetNewContests", contestResult);
        }

    }
}