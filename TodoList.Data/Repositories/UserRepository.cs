using Dapper;
using Npgsql;
using System.Threading.Tasks;
using ToDoList.Model;


namespace ToDoList.Data.Repositories
{
    public class UserRepository : IUserRepository   
    {
        private DBConnection _connectionString;
        public UserRepository(DBConnection connectinString)
        {
            _connectionString = connectinString;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> CreateUser(User user)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO users (username, password, created)
                        VALUES (@username, @password, @created )
                        ";

            var result = await db.ExecuteAsync(sql, new { user.Username, user.Password, user.Created });

            return result > 0;
        }

        public async Task<User> LoguinUser(string username, string password)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT * FROM users
                        WHERE username = @Username
                          AND password = @Password
                        ";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Username = username, Password = password });
            
        }
    }
}
