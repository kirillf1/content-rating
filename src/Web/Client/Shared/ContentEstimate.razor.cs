using Microsoft.AspNetCore.Components;
using Web.Shared;
using Web.Shared.Rating;
using Web.Shared.Rooms;

namespace Web.Client.Shared
{
    public partial class ContentEstimate
    {
        [Parameter]
        public int ContentNumber { get; set; }
        [Parameter]
        public Content Content { get; set; } = default!;
        [Parameter]
        public List<User> Users { get; set; } = default!;
        [Parameter]
        public List<RatedContent> RatedContent { get; set; } = default!;
        [Parameter]
        public EventCallback<ContentWithRating> Estimated { get; set; }
        List<UserAndRating> UserAndRatings { get; set; } = new List<UserAndRating>();
        [Parameter]
        public EventCallback<List<User>> UsersChanged { get; set; }
        private bool ContentHidden { get; set; } = true;
        private string ContentUrl { get; set; } = default!;
        public double AvarageRating { 
            get 
            {
                
                if (UserAndRatings.Count == 0)
                    return 0;
                return Math.Round( UserAndRatings.Average(c => c.Rating),2);
            }
           
        }
        protected override void OnParametersSet()
        {
            ContentUrl = Content.Url + "?autoplay=1";
            UserAndRatings = RatedContent.Join(Users, r => r.UserId, u => u.Id, (r, u) => new UserAndRating(u, r.Rating, r.CanEstimate)).ToList();
            
        }
        }
        public class UserAndRating
        {
            
            public int UserId { get; set; }
            public User User { get; set; }

            public UserAndRating(User user,double rating,bool canEstimate)
            {
                User = user;
                UserId = User.Id;
                Rating = rating;
                CanEstimate = canEstimate;
                
            }

            public double Rating { get; set; }
            public bool CanEstimate { get; set; }
        }
    }

