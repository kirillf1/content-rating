using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
namespace Web.Shared.GuessContent
{
    public class GuessContentClient
    {
        private class CurrentContentPriority
        {
            public CurrentContentPriority(int priority,ContentGuessInfo contentGuessInfo)
            {
                Priority = priority;
                Content = contentGuessInfo;
            }
            public int Priority { get; set; }
            public ContentGuessInfo Content { get; }

        }
        public const string Content_Raw_Url = "/api/contentguess/RawContent";
        public const string Content_ById_Url = "/api/contentguess/ById";
        public Action? OnContentEmtpy { get; set; }
        public int ContentSize { get; set; }
        private readonly HttpClient httpClient;
        List<CurrentContentPriority> CurrentContentRaw { get; set; }
        private List<int> SelectedTagIds { get; set; }
        private Queue<long> ContentIds { get; set; }
        private Queue<ContentGuess> ContentGuessQueue { get; set; }
        public GuessContentClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            CurrentContentRaw = new List<CurrentContentPriority>();
            SelectedTagIds = new List<int>();
            ContentIds = new Queue<long>();
            ContentGuessQueue = new Queue<ContentGuess>();
            ContentSize = 5;
        }
        public async Task ChangeTags(IEnumerable<int> tagIds)
        {
           
          SelectedTagIds.Clear();
          CurrentContentRaw.Clear();
          ContentIds.Clear();
          ContentGuessQueue.Clear();
          SelectedTagIds.AddRange(tagIds);
          await LoadRawContent();
          
            
        }
        public async Task<ContentGuessFalseNames?> GetContent(int falseNamesCount)
        {
            if (CurrentContentRaw.Count == 0)
                await LoadRawContent();
            var content = await LoadContentForGuess();
            if (content == null)
            {
                OnContentEmtpy?.Invoke();
                return default;
            }
            var falseNames =  CurrentContentRaw.OrderByDescending(x => x.Priority).ThenBy(c=> Guid.NewGuid()).Where(c=>c.Content.Id!=content.Id).Take(falseNamesCount).ToList();
            falseNames.ForEach(c => c.Priority--);
            return new ContentGuessFalseNames()
            {
                Content = content,
                ContentStartTime = content.ContentStartSeconds,
                FalseNames = falseNames.Select(c => c.Content.Name).ToList()
            };
        }
        private async Task LoadRawContent()
        {
            string tagQuery = String.Empty;
            if (SelectedTagIds.Count > 0)
                tagQuery = $"tagId={ string.Join("&tagId=", SelectedTagIds)}";
           
            var result = await httpClient.GetFromJsonAsync<IEnumerable<ContentGuessInfo>>(
                $"{Content_Raw_Url}?contentCount=1000&needShuffle=true&{tagQuery}");
            if (result == null)
                throw new ArgumentException();
            CurrentContentRaw.AddRange(result.Select(c => new CurrentContentPriority(100, c)));
            ContentIds.Clear();
            CurrentContentRaw.ForEach(c => ContentIds.Enqueue(c.Content.Id));
        }
        private async Task<ContentGuess?> LoadContentForGuess()
        {
            if (ContentGuessQueue.TryDequeue(out ContentGuess? content))
            {
                if (ContentGuessQueue.Count < 3)
                {
                    var task = AddContentInQueue(ContentSize);
                    
                }
                return content;
            }
            else
            {
                var result = await AddContentInQueue(ContentSize);
                if (result)
                    return ContentGuessQueue.Dequeue();
            }
            return default;
        }
        private async Task<bool> AddContentInQueue(int count)
        {
            int contentSize = count;
            if (ContentIds.Count == 0)
            {
                
                return false;
            }
            else if (ContentIds.Count < contentSize)
            {
                contentSize = ContentIds.Count - contentSize;
            }
            var arr = new long[contentSize];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = ContentIds.Dequeue();
            }
            var result = await httpClient.GetFromJsonAsync<IEnumerable<ContentGuess>>($"{Content_ById_Url}?contentId={string.Join("&contentId=", arr)}");
            if (result != null)
            {
                foreach (var item in result)
                {
                    ContentGuessQueue.Enqueue(item);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
