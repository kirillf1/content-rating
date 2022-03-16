using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        ContentGuessService ContentGuessService { get; set; } = default!;
        [Inject]
        TagService TagService { get; set; } = default!;
        private int CorrectAsnwerStreak { get; set; } = 0;
        private int AnswerCount { get; set; } = 0;
        private int CorrectAnswer { get; set; }
       
        private int IncorrectAnswer { get; set; }
        private int contentStopSeconds { get; set; } = 60;
        private bool StopPlay { get; set; } = false;
        
        System.Timers.Timer myTimer { get; set; } = new System.Timers.Timer();
       
        private List<Tag> Tags { get; set; } = new List<Tag>();
        List<int> SelectedTags { get; set; } = new List<int>();
        public ContentGuessFalseNames? CurrentContentGuess { get; set; }
        private Stack<ContentGuessFalseNames> ContentCollection { get; set; } = new Stack<ContentGuessFalseNames>();
        private List<string>? Answers { get; set; } 
        private bool isAnswered { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            Tags = await TagService.GetAllTags()?? new List<Tag>();
            myTimer.Elapsed +=  async(o, e) => { StopPlay = true; if (ContentPlay != null) await ContentPlay.Pause();  
                myTimer.Stop(); StateHasChanged();
            };
        }
       
        
        private void AddTags(IEnumerable<TagTree> tags)
        {
            foreach (var tag in tags)
            {
                tag.Children.AddRange(Tags.Where(c => c.ParentId == tag.Tag.Id).Select(t => new TagTree(t)));
               if(tag.Children.Count > 0)
                  AddTags(tag.Children);
            }
        }
        private async Task GetContent()
        {
            isAnswered = false;

            if (CurrentContentGuess != null && ContentPlay != null)
            {
                await ContentPlay.StopPlay();
                myTimer.Stop();
            }
            CurrentContentGuess = null;
            StopPlay = false;
            if (ContentCollection.Count > 0)
            {
               await configureContent( ContentCollection.Pop());
                if(ContentCollection.Count <3)
                await LoadContent();
            }
            else
            {
              await LoadContent();
              await  configureContent(ContentCollection.Pop());
            }
            
            
        }
        private void TagSelected(TagTree tagTree)
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
             ContentCollection.Clear();
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
        private async Task configureContent(ContentGuessFalseNames contentGuess)
        {
            Answers = new List<string>(contentGuess.FalseNames);
            Answers.Add(contentGuess.Content.Name);
            Answers = Answers.OrderBy(c => Guid.NewGuid()).ToList();
            if (ContentPlay != null)
               await ContentPlay.UpdateContent(contentGuess);
            CurrentContentGuess = contentGuess;
            StateHasChanged();
        }
        private async void  CheckAnswer(string answer)
        {
            isAnswered = true;
            if (ContentPlay != null)
                await ContentPlay.StartPlay();
            myTimer.Stop();
            AnswerCount++;
            if (CurrentContentGuess!.Content.Name == answer)
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
        private async Task LoadContent()
        {
           var contentForGuess = await ContentGuessService.GetContentGuess(300, 9, SelectedTags);
                if (contentForGuess != null)
                {
                    foreach (var item in contentForGuess)
                    {
                        ContentCollection.Push(item);
                    }
                }
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
