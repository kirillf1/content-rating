namespace Rating.Domain
{
    public class Room
    {
        public Room(Guid id, string name)
        {
            Id = id;
            Name = name;
            Users = new List<User>();
            Contents = new List<Content>();
            CreationTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Content> Contents { get; set; }
        public bool IsComplited { get; set; }
        public DateTime CreationTime { get; private set; }

        
    }
}