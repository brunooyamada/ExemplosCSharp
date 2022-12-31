using ApiAuth.Models;

namespace ApiAuth.Repository
{
    public class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new () { Id = 1, Username = "batman", Password = "batman", Role = "manager" },
                new () { Id = 2, Username = "robin", Password = "robin", Role = "employee" },
            };
            return users
                .Where(x =>
                    x.Username.ToLower() == username.ToLower()
                    && x.Password == password
                 )
                .FirstOrDefault();
        }
    }
}
