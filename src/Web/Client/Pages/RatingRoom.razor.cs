using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Web.Shared;
using Web.Shared.Rating;
using Web.Shared.Rooms;

namespace Web.Client.Pages
{
    public partial class RatingRoom
    {
        [Inject]
        IJSRuntime JS { get; set; } = default!;
        [Parameter]
        public string Id { get; set; } = default!;
        [Inject]
        NavigationManager navigationManager { get; set; } = default!;
        [Inject]
        State State { get; set; } = default!;
        [Inject]
        RatingClientHub RatingClientHub { get; set; } = default!;
        Room Room { get; set; } = default!;
        private bool MenuOpened { get; set; } = false;
        Dictionary<Content, List<RatedContent>> Content { get; set; } = new Dictionary<Content, List<RatedContent>>();
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (State.User == null)
                    return;
                await RatingClientHub.Init();
                await RatingClientHub.JoinRoom(Id, State.User.Id);
            }
        }
        protected override void OnInitialized()
        {
            RatingClientHub.UserKicked += RatingClientHub_UserKicked;
            RatingClientHub.RoomEstimated += RatingClientHub_RoomEstimated;
            RatingClientHub.RoomLoaded += RoomChanged;
            RatingClientHub.ContentUsersLoaded += RatingLoaded;
            RatingClientHub.ContentRatingChanged += ContentRatingChanged;
            RatingClientHub.UsersChanged += UsersChanged;
        }
        private async void EndEstimate()
        {
            if (Room.CreatorId == State.User!.Id)
               await RatingClientHub.EndRating(State.User!.Id, Id);
        }
        private async void RatingClientHub_RoomEstimated(string id)
        {
            await DisposeAsync();
            navigationManager.NavigateTo("/resultroom/" + id);
        }

        private async void RatingClientHub_UserKicked(int userId)
        {
            if (State.User!.Id == userId)
            {
                await DisposeAsync();
                navigationManager.NavigateTo(navigationManager.BaseUri);
            }
               
        }

        public async Task ChangeRating(ContentWithRating rating)
        {
            var content = Content.First(c => c.Key.Id == rating.ContentId);
            content.Value.First(r => r.UserId == rating.UserId).Rating = rating.Rating;
            await RatingClientHub.UpdateRatingContent(Id, rating.UserId, rating.ContentId, rating.Rating);
            //StateHasChanged();
        }
        private async Task LeaveRoom()
        {
            await RatingClientHub.LeaveRoom(Id, State.User!.Id);
            await DisposeAsync();
            navigationManager.NavigateTo(navigationManager.BaseUri);
        }
        private void UsersChanged(User user,string command)
        {
            var oldUser = Room.Users.FirstOrDefault(c=>c.Id == user.Id);
            if (command == "out" && oldUser!=null)
            {
                Room.Users.Remove(oldUser);
                foreach (var content in Content)
                {
                    content.Value.Remove(content.Value.First(c => c.UserId == user.Id));
                }
            }
            else if (command == "in")
            {
                if (oldUser == null)
                {
                    Room.Users.Add(user);
                    foreach (var content in Content)
                    {
                        content.Value.Add(new RatedContent() { CanEstimate = false, Rating = 0, UserId = user.Id });
                    }
                }
            }
            StateHasChanged();
        }

        private void ContentRatingChanged(ContentWithRating rating)
        {
            
            var content = Content.First(c => c.Key.Id == rating.ContentId);
            content.Value.First(r => r.UserId == rating.UserId).Rating = rating.Rating;
            this.StateHasChanged();
        }

        private void RoomChanged(Room room )
        {
            Room = room;
            Content.Clear();
            this.StateHasChanged();
        }
        private void RatingLoaded(List<UsersRating> usersRatings)
        {
            Content.Clear();
            foreach (var content in Room.Contents.Join(usersRatings, c => c.Id, u => u.ContentId, (c, u) =>new {c, u }))
            {
                if (Room.IsSingleRoom)
                    content.u.RatedContent.ForEach(c => c.CanEstimate = true);
                else
                    content.u.RatedContent.ForEach(c => { if (c.UserId == State.User!.Id) c.CanEstimate = true; });
                 Content.Add(content.c, content.u.RatedContent);
            }
            this.StateHasChanged();
        }

        public async void KickUser(User user)
        {
            if(State.User!.Id == Room.CreatorId) 
            {
                await RatingClientHub.KickFromRoom(user.Id, Id);
            }
            
        }
        public async ValueTask DisposeAsync()
        {
            RatingClientHub.UserKicked -= RatingClientHub_UserKicked;
            RatingClientHub.RoomEstimated -= RatingClientHub_RoomEstimated;
            RatingClientHub.RoomLoaded -= RoomChanged;
            RatingClientHub.ContentUsersLoaded -= RatingLoaded;
            RatingClientHub.ContentRatingChanged -= ContentRatingChanged;
            RatingClientHub.UsersChanged -= UsersChanged;
            await RatingClientHub.DisposeAsync();
        }
        private async Task ShowProgress()
        {
            var result = string.Join("\n", Content.OrderByDescending(c => c.Value.Average(c => c.Rating)).
                Select((c, i) => $"№{i + 1} {c.Key.Name} Оценка: {Math.Round(c.Value.Average(n => n.Rating), 2)}"));
            await JS.InvokeVoidAsync("alert", "Остальные можно посмотреть в консоли \n" + result);
        }
    }
}
