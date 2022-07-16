using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Data.Repositories
{
    public class RPList
    {
        public static List<AList> _AList = new List<AList>()
        {            
        };

        public IEnumerable<AList> ListAll()
        {
            return _AList;
        }

        public AList GetTheList(string title)
        {
            var list = _AList.Where(lis => lis.Title == title);

            return list.FirstOrDefault();
        }

        public void AddItem(AList newList)
        {
            _AList.Add(newList);
        }

    }
}


