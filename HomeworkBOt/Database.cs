using System.Text.Json;
namespace HomeworkBOt
{
    public class Database
    {
        public static string basePath = @"C:\Users\user\Desktop\base.json";
        // database sifatida json filedan foydalanamiz

        public void Create(User user)
        {
            List<User> users = GetAllUsers();
            if (users.Any(c => c.ChatId == user.ChatId))
            {
                return;
            }
            users.Add(user);
            SaveChats(users);
        }
        public string Read(long id)
        {
            List<User> users = GetAllUsers();
            var user = users.Find(u => u.ChatId == id);
            if (user != null)
            {
                return $"{user.ChatId} : {user.username}";
            }
            else
            {
                return $"{user.ChatId} : {user.ChatId}";
            }
        }

        public void Update(long id,string text)
        {
            List<User> users = GetAllUsers();
            var user = users.Find(u => u.ChatId == id);
            if(user != null)
            {
                user.username = text;
                SaveChats(users);
            }

        }

        public void Delete(long id, string text)
        {
            List<User> users = GetAllUsers();
            var user = users.Find(u => u.ChatId == id);
            if (user != null)
            {
                users.Remove(user);
                SaveChats(users);
            }

        }

        private List<User> GetAllUsers()
        {
            if (System.IO.File.Exists(basePath))
            {
                string json = System.IO.File.ReadAllText(basePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>;
            }
            else
            {
                return new List<User>();
            }
        }
        private void SaveChats(List<User> users)
        {
            string json = JsonSerializer.Serialize(users);
            System.IO.File.WriteAllText(basePath, json);
        }
        // agar catoliklar bo'lmasa CRUD yakunlandi
    }

    public class User
    {
        public long ChatId { get; set; }
        public string? username { get; set; }
    }
}
