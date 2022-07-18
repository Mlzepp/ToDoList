using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Data.Repositories
{
    internal interface IUserRepository
    {
        Task<bool> CreateUser(User user);
        Task<bool> LoguinUser(User user);
    }
}
