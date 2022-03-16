namespace ContentGuess.Application.Dto
{
    public class ContentForGuess
    {
        public ContentForGuess(ContentRead content, IEnumerable<string> names,double? contentStartTime)
        {
            Content = content;
            FalseNames = new List<string>(names);
            ContentStartTime = contentStartTime;
        }
        public ContentRead Content { get; set; }
        public List<string> FalseNames { get; set; }
        public double? ContentStartTime { get; set; }
    }
}
