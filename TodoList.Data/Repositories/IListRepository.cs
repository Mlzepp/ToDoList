using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Data.Repositories
{
    public interface IListRepository
    {
        Task<IEnumerable<AList>> GetAllItems();
        Task<AList> GetItemDetails(int id);
        Task<bool> InsertItem(AList list);
        Task<bool> UpdateItem(AList list);
        Task<bool> DeleteItem(AList list);

    }
}
