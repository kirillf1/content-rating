using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;
using Web.Client.Shared.ContentPlay;
using Web.Shared.GuessContent;

namespace Web.Client.Pages
{
    public partial class GuessContent
    {
        IContentPlay? ContentPlay;
        [Inject]
        IJSRuntime JS { get; set; } = default!;
        [Inject]
        GuessContentClient GuessContentClient { get; set; } = default!;
        [Inject]
        TagService TagService { get; set; } = default!;
        private int CorrectAsnwerStreak { get; set; } = 0;
        private int AnswerCount { get; set; } = 0;
        private int CorrectAnswer { get; set; }
        private bool AnswerByNumber { get; set; }
        public string AnswerNumber { get; set; } = string.Empty;
        private int IncorrectAnswer { get; set; }
        private int contentStopSeconds { get; set; } = 60;
        private bool StopPlay { get; set; } = false;
        System.Timers.Timer myTimer { get; set; } = new System.Timers.Timer();
       
        private List<Tag> Tags { get; set; } = new List<Tag>();
        List<int> SelectedTags { get; set; } = new List<int>();
        public ContentGuessFalseNames? CurrentContentGuess { get; set; }
       
        private List<string>? Answers { get; set; } 
        private bool isAnswered { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            Tags = await TagService.GetAllTags()?? new List<Tag>();
            myTimer.Elapsed +=  async(o, e) => { StopPlay = true; if (ContentPlay != null) await ContentPlay.Pause();  
                myTimer.Stop(); StateHasChanged();
            };
            GuessContentClient.OnContentEmtpy += async () => await JS.InvokeVoidAsync("alert", "Контент закончился, поменяйте теги");
        }
       
        private async Task GetContent()
        {
            isAnswered = false;
            if (CurrentContentGuess != null && ContentPlay != null)
            {
                await ContentPlay.StopPlay();
                myTimer.Stop();
            }
            CurrentContentGuess = await GuessContentClient.GetContent(10);
            if (CurrentContentGuess == null)
                return;
            StopPlay = false;
            Answers = new List<string>(CurrentContentGuess.FalseNames);
            Answers.Add(CurrentContentGuess.Content.Name);
            Answers = Answers.OrderBy(c => Guid.NewGuid()).ToList();
            StateHasChanged();
            if (ContentPlay != null)
                await ContentPlay.UpdateContent(CurrentContentGuess);


        }
        private async Task TagSelected(TagTree tagTree)
        {
             var id = tagTree.Tag.Id;
             if (SelectedTags.Contains(id))
             {
                 SelectedTags.Remove(id);
             }
             else
             {
                SelectedTags.RemoveAll(t => FindTagsDown(tagTree).Contains(t));
                if (tagTree.Tag.ParentId.HasValue)
                    SelectedTags.RemoveAll(t => FindTagsUp(tagTree.Tag).Contains(t));
                SelectedTags.Add(id);
            }
            await GuessContentClient.ChangeTags(SelectedTags);
            StateHasChanged();
            
        }
        private IEnumerable<int> FindTagsUp(Tag tag)
        {
            List<int> tagsIds = new List<int>();
            tagsIds.Add(tag.Id);
            if (tag.ParentId != null)
            {
                var tags = Tags.Where(t => t.Id == tag.ParentId);
                foreach (var tagParent in tags)
                {
                    tagsIds.AddRange(FindTagsUp(tagParent));
                }
            }
            return tagsIds;
        }
        private IEnumerable<int> FindTagsDown(TagTree tag)
        {
            List<int> tagsIds = new List<int>();
            tagsIds.Add(tag.Tag.Id);
            if (tag.Children.Count > 0)
            {
                foreach (var childTag in tag.Children)
                {
                    tagsIds.AddRange(FindTagsDown(childTag));
                }
            }
            return tagsIds;
        }
        private async void  CheckAnswer(string answer)
        {
            isAnswered = true;
            if (ContentPlay != null)
                await ContentPlay.StartPlay();
            myTimer.Stop();
            AnswerCount++;
            bool result = AnswerByNumber ? CheckAnswerByNumber(answer) : CheckAsnwerByName(answer);
            if (result)
            {
                CorrectAnswer++;
                CorrectAsnwerStreak++;
                await JS.InvokeVoidAsync("alert", "вы правильно ответили!");
            }
            else
            {
                CorrectAsnwerStreak = 0;
                IncorrectAnswer++;
                await JS.InvokeVoidAsync("alert", $"вы ошиблись! Правильный ответ:{CurrentContentGuess!.Content.Name}");
            }
        }
        private bool CheckAsnwerByName(string answer) => CurrentContentGuess!.Content.Name == answer;
        private bool CheckAnswerByNumber(string number)
        {
           var res = Regex.Match(CurrentContentGuess!.Content.Name, @"\d+");
            if (res.Success)
            {
               return res.Value == number;
            }
            return false;
        }


        private async Task startTimer()
        {
            if (ContentPlay != null)
                await ContentPlay.StartPlay();
            if (contentStopSeconds > 0)
            {
                myTimer.Interval = (contentStopSeconds)* 1000;
                myTimer.Start();
            }
        }
    }
}
