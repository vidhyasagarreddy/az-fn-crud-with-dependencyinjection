using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement
{
    public class UserService
    {
        private List<User> users;

        public UserService()
        {
            users = new List<User>();
        }

        public bool Add(User user)
        {
            users.Add(user);

            // Ideally this is not the case
            return true;
        }

        public List<User> GetAll()
        {
            return users;
        }

        public bool Delete(string email)
        {
            var user = users.Find(m => m.Email == email);
            if(user != null)
            {
                return users.Remove(user);
            }

            return false;
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
