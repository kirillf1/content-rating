using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Web.Shared.GuessContent;

namespace Web.Client.Shared.ContentPlay
{
    public partial class YoutubePlay : IContentPlay
    {
       
        [Parameter]
        public bool PlayerStoped { get; set; }
        [Parameter]
        public EventCallback ContentLoaded { get; set; }
        [Parameter]
        public bool IsAnswered { get; set; }
        [Parameter]
        public ContentGuessFalseNames CurrentContentGuess { get; set; } = default!;
        [Inject]
        IJSRuntime JS { get; set; } = default!;
        private string PlayButtonClass => isPlay ? "oi oi-media-pause" : "oi oi-media-play";
        private string videoStyle => IsAnswered ? "position: absolute;width: 100%;height: 100%;" : "height:1px;width:1px;opacity:0;";
        private bool isPlay { get; set; } = false;

       
        public async Task PauseControl()
        {
            if (!isPlay)
                await StartPlay();
            else
                await Pause();
        }
        public async Task ShowAnswer()
        {
            
            
            StateHasChanged();
            await StartPlay();
            
        }
        public async Task Pause()
        {
            await JS.InvokeVoidAsync("pause", "youtube-video");
            isPlay = false;
            StateHasChanged();
        }
        public async Task StartPlay()
        {
            await JS.InvokeVoidAsync("play", "youtube-video");
            isPlay = true;
            StateHasChanged();
        }

        public async Task StopPlay()
        {
            await JS.InvokeVoidAsync("stop", "youtube-video");
            isPlay = false;
            StateHasChanged();
        }

        public Task UpdateContent(ContentGuessFalseNames contentGuess)
        {
            Console.WriteLine("update");
            contentGuess.Content.Url += $"?start={contentGuess.ContentStartTime.GetValueOrDefault()}" +
                "&&enablejsapi=1&version=3&playerapiid=ytplayer";
            CurrentContentGuess = contentGuess;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
