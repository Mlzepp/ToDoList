using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Data
{
    public class DBConnection
    {
        public DBConnection(string connectionString) => ConnectionString = connectionString;
        public string ConnectionString { get; set; }

    }
}
