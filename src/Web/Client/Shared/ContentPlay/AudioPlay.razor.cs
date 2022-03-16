using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Web.Shared.GuessContent;

namespace Web.Client.Shared.ContentPlay
{
    public partial class AudioPlay : IContentPlay
    {
       
        public ContentGuessFalseNames CurrentContentGuess { get; set; } = default!;
        [Parameter]
        public bool PlayerStoped { get; set; } = false;
        [Parameter]
        public EventCallback ContentLoaded { get; set; }
        [Parameter]
        public bool IsAnswered { get; set; }
        [Inject]
        IJSRuntime JS { get; set; } = default!;
        private string PlayButtonClass => isPlay ? "oi oi-media-pause" : "oi oi-media-play";
        private bool isPlay { get; set; } = true;
        public async Task PauseControl()
        {
            if (!isPlay)
                await StartPlay();
            else
                await Pause();
        }
        public async Task ShowAnswer()
        {
            IsAnswered = true;
            await StartPlay();
        }

        public async Task StartPlay()
        {
            isPlay = true;
            await JS.InvokeVoidAsync("playAudio", "Audio");
        }

        public async Task StopPlay()
        {
            isPlay = false;
            await JS.InvokeVoidAsync("stopAudio", "Audio");
        }
        public async Task StartTimer()
        {
           await ContentLoaded.InvokeAsync();
        }

        public async Task Pause()
        {
            isPlay = false;
            await JS.InvokeVoidAsync("pauseAudio", "Audio");
        }

        public Task UpdateContent(ContentGuessFalseNames contentGuess)
        {
            contentGuess.Content.Url += $"#t={CurrentContentGuess.ContentStartTime.GetValueOrDefault()}";
            CurrentContentGuess = contentGuess;
                
            return Task.CompletedTask;
        }
    }
}
