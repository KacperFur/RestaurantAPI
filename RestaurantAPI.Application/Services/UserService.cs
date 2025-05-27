using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Services
{
    public class UserService : IUserService
    {
        private static List<Role> roles = new List<Role>
        {
            new Role(0,"Customer","Allows to make an order"),
            new Role(1,"Employee","Regular Employee"),
            new Role(2,"Supervisor","Allows to make an order"),
            new Role(3,"Manager","Allows to make an order"),
            new Role(4,"Administrator","System administrator")
        };

        private static List<User> users = new List<User>
        {
            new User(1,"John","Paul","JohnPaul","psswd","johnpaul37@mail.com",roles.FirstOrDefault(e=>e.Id==1)),
            new User(1,"Eric","Cartman","Erixx","pswd1","eric@mail.com",roles.FirstOrDefault(e=>e.Id==2)),
            new User(1,"","","","","",roles.FirstOrDefault(e=>e.Id==3)),
            new User(1,"","","","","",roles.FirstOrDefault(e=>e.Id==4))
        };

        public void Create(User user)
        {
            users.Add(user);
        }

        public bool Delete(int id)
        {
            var user = users.FirstOrDefault(e => e.Id == id);
            if (user == null)
                return false;

            users.Remove(user);
            return true;
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User GetById(int id)
        {
            var user = users.FirstOrDefault(e => e.Id == id);
            return user;
        }

        public bool Update(int id, User user)
        {
            var existingUser = users.FirstOrDefault(e => e.Id == id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Username = user.Username;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            return true;
        }
    }
}
