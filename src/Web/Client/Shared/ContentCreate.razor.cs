using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Web.Shared.Rooms;

namespace Web.Client.Shared
{
    public partial class ContentCreate
    {
        [Parameter]
        public Content Content { get; set; } = default!;
        [Parameter]
        public EventCallback<Content> ContentRemoved { get; set; }
        private bool IsContentOpened { get; set; } = false;
        public void OpenContent()
        {
            IsContentOpened  = !IsContentOpened;
        }
    }
}
