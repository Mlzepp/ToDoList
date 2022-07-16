using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Data.Repositories
{
    public class ListRepository : IListRepository
    {
        private DBConnection _connectionString;
        public ListRepository(DBConnection connectinString)
        {
            _connectionString = connectinString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteItem(AList list)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE
                        FROM list 
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new { Id = list.Id });

            return result > 0;
        }

        public async Task<IEnumerable<AList>> GetAllItems()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT id, title, description, duedate, completion_date, status, created_by  
                        FROM list ";

            return await db.QueryAsync<AList>(sql, new { });

        }

        public async Task<AList> GetItemDetails(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT id, title, description, duedate, completion_date, status, created_by  
                        FROM list 
                        WHERE id = @Id";

            return await db.QueryFirstOrDefaultAsync<AList>(sql, new { Id = id });
        }

        public async Task<bool> InsertItem(AList list)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO list (title, description, duedate, completion_date, status, created_by)
                        VALUES (@title, @description, @duedate, @completion_date, @status, @created_by )
                        ";

            var result = await db.ExecuteAsync(sql, new { list.Title, list.Description, list.DueDate, list.Completion_Date, list.Status, list. Created_By });

            return result > 0;
        }

        public async Task<bool> UpdateItem(AList list)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE list
                        SET title = @title,
                            description = @description, 
                            duedate = @duedate, 
                            completion_date = @completion_date, 
                            status = @status, 
                            created_by = @created_by
                        WHERE id = @id
                        ";

            var result = await db.ExecuteAsync(sql, new { list.Title, list.Description, list.DueDate, list.Completion_Date, list.Status, list.Created_By, list.Id});

            return result > 0;
        }
    }
}
