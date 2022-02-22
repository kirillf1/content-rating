namespace Rating.Domain
{
    public class Room
    {
        public Room(Guid id, string name,bool isSingleRoom)
        {
            Id = id;
            Name = name;
            IsSingleRoom = isSingleRoom;
            Users = new List<User>();
            Contents = new List<Content>();
            CreationTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            IsPrivate = true;
        }
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Content> Contents { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreationTime { get; private set; }
        public bool IsSingleRoom { get; set; } 
        public int CreatorId { get; set; }
        public bool IsPrivate { get; set; }
        public void AddUsers(IEnumerable<User> users)
        {
            Users.AddRange(users);
        }
        public void AddContent(IEnumerable<Content> contents)
        {
            Contents.AddRange(contents);
        }
        public void AddContent(Content content)
        {
            Contents.Add(content);
        }
        public void AddUser(User user)
        {
            Users.Add(user);
        }
        public void DeleteUser(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                Console.WriteLine(user.Id + "was deleted from room");
                Users.Remove(user);
            }
        }
        public void DeleteContent(long id)
        {
            var content = Contents.FirstOrDefault(c => c.Id == id);
            if(content != null)
            Contents.Remove(content);
        }
        public void DeleteContent(IEnumerable<Content> contents)
        {
            foreach (var content in contents)
            {
                DeleteContent(content.Id);
            }
          
        }
    }
}