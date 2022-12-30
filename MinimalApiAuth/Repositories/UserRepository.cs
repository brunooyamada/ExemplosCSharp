using MinimalApiAuth.Models;

namespace MinimalApiAuth.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password, string role) 
        {
            var users = new List<User>
            {
                new User { Id = 1, UserName = "batman", Password = "batman", Role = "manager" },
                new User { Id = 2, UserName = "robin" , Password = "robin", Role = "employee" },
            };
            return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
