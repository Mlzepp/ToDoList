using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Data.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(User user);
        Task<User> LoguinUser(string username, string password);
    }
}
