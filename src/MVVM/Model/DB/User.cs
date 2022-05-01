using System.ComponentModel.DataAnnotations;

namespace StudentSystemWinForms.MVVM.Model.DB
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public Settings Settings { get; set; }
        public User(string username, string displayName, string password)
        {
            Username = username;
            DisplayName = displayName;
            Password = password;
            Settings = new Settings();
        }

        public User()
        {
        }
    }
}
