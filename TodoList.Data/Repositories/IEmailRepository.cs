using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Data.Repositories
{
    public interface IEmailRepository
    {
        Task<bool> SendEmail(string email, int id, string title, string description, DateTime dueDate);

        Task<AList> GetId();

        Task<User> GetEmail(string user);
    }
}
