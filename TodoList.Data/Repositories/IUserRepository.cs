using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Data.Repositories
{
    public interface IUserRepository
    {
        Task<bool> RegisterUser(User user);
        Task<bool> LoginUser(User user);
    }
}
