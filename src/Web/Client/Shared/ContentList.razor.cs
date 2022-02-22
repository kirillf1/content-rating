using Microsoft.AspNetCore.Components;
using Web.Shared;
using Web.Shared.Rating;
using Web.Shared.Rooms;

namespace Web.Client.Shared
{
    public partial class ContentList
    {

        [Parameter]
        public Dictionary<Content, List<RatedContent>> Content { get; set; } = default!;
        private IEnumerable<(KeyValuePair<Content, List<RatedContent>> content, int pos)> contentWithPos = default!;
        [Parameter]
        public List<User> Users { get; set; } = default!;
        [Parameter]
        public EventCallback<ContentWithRating> ContentEstimated { get; set; }
        protected override void OnParametersSet()
        {
            contentWithPos = Content.Select((c, i) => (content: Content.First(key=>key.Key.Id == c.Key.Id), pos: ++i));
        }
    }
}
