namespace ContentGuess.Application.Dto
{
    public class ContentForUpdate
    {
        public ContentForUpdate(string name, string url, List<int> selectedTags, int contentTypeId, int? authorId)
        {
           
            Url = url;
            SelectedTags = selectedTags;
            Name = name;
            ContentTypeId = contentTypeId;
            AuthorId = authorId;
        }
        public string Url { get; set; }
        public List<int> SelectedTags { get; set; }
        public string Name { get; set; }
        public int ContentTypeId { get; set; }
        public int? AuthorId { get; set; }
    }
}
