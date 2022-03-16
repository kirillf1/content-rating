using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Web.Shared;
using Web.Shared.Rating;
using Web.Shared.Rooms;

namespace Web.Client.Pages
{
    public partial class ResultRoom
    {
        [Parameter]
        public string Id { get; set; } = default!;
        [Inject]
        State State { get; set; } = default!;
        [Inject]
        RatingClientHub RatingClientHub { get; set; } = default!;
        [Inject]
        IJSRuntime JS { get; set; } = default!;
        Room Room { get; set; } = default!;
       
        Dictionary<Content, List<RatedContent>> Content { get; set; } = new Dictionary<Content, List<RatedContent>>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await RatingClientHub.Init();
                await RatingClientHub.JoinRoom(Id, 0);
            }
        }
        protected override void OnInitialized()
        {
            RatingClientHub.RoomLoaded += RoomChanged;
            RatingClientHub.ContentUsersLoaded += RatingLoaded;
        }
 
        private void RoomChanged(Room room)
        {
            Room = room;
            Content.Clear();
          
        }
        private void RatingLoaded(List<UsersRating> usersRatings)
        {
            Content.Clear();
            foreach (var content in Room.Contents.Join(usersRatings, c => c.Id, u => u.ContentId, (c, u) => new { c, u })
                .OrderByDescending(c=>c.u.RatedContent.Average(c=>c.Rating)))
            {
                content.u.RatedContent.ForEach(c => c.CanEstimate = false);
                Content.Add(content.c, content.u.RatedContent);
            }
            this.StateHasChanged();
        }
        public async ValueTask DisposeAsync()
        {
            
            RatingClientHub.RoomLoaded -= RoomChanged;
            RatingClientHub.ContentUsersLoaded -= RatingLoaded;
            await RatingClientHub.DisposeAsync();
        }
        private async Task Print()
        {
            await JS.InvokeVoidAsync("print");
        }
    }
}
