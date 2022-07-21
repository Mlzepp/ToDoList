using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }    
        public DateTime Created { get; set; }
    }
}
