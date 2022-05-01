using System.Data.Entity;
using System.Linq;
using StudentSystemWinForms.MVVM.Model.DB;
using StudentSystemWinForms.Utils;

namespace StudentSystemWinForms.DAL
{
    public class UserService
    {
        private readonly StudentContext _studentContext;
        public UserService(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }
        public bool Login(string username, string password)
        {
            var user = _studentContext.Users
                .Where(u => u.Username == username)
                .Include("Settings")
                .FirstOrDefault();
            UserInfo.CurrentUser = user;
            return user != null && user.Password.Equals(Cryptography.EncodePassword(password));
        }

        public bool UpdateUserSettings(int userId, Settings settings)
        {
            var user = _studentContext.Users.Find(userId);
            if (user == null) return false;
            user.Settings = settings;
            _studentContext.Entry(user).State = EntityState.Modified;
            _studentContext.SaveChanges();
            return true;
        }

        public void Register(string loginUsername, string password)
        {
            var user = _studentContext.Users.FirstOrDefault(u => u.Username == loginUsername);
            if (user == null)
            {
                _studentContext.Users.Add(new User(loginUsername, loginUsername, Cryptography.EncodePassword(password)));
                _studentContext.SaveChanges();
            }
            UserInfo.CurrentUser = _studentContext.Users.FirstOrDefault(u => u.Username == loginUsername);
        }
    }
}
