using Rating.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers.ObjectFactories
{
    public static class UserFactory
    {
        static List<string> Names { get; set; } = new() { "kirill", "david", "kantic", "denis", "jenya", "danil", "kostya" };
        static Random Random = new Random();
        public static User CreateUser()
        {
            return new User(Names[Random.Next(0, Names.Count)], Random.Next().ToString(), Names[Random.Next(0, Names.Count)] + "@gmail.ru");
        }
        public static List<User> CreateUsers(int count)
        {
            var users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                users.Add(CreateUser());
            }
            return users;
        }
    }
}
