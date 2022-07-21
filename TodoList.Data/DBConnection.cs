namespace ToDoList.Data
{
    public class DBConnection
    {
        public DBConnection(string connectionString) => ConnectionString = connectionString;
        public string ConnectionString { get; set; }

    }
}
