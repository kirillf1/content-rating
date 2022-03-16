using Web.Shared.GuessContent;

namespace Web.Client.Shared.ContentPlay
{
    public interface IContentPlay
    {
        public Task Pause();
        public Task StopPlay();
        public Task StartPlay();
        public Task UpdateContent(ContentGuessFalseNames contentGuess);
    }
}
