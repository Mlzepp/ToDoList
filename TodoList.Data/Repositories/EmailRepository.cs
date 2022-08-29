using System;
using System.Threading.Tasks;
using Dapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Npgsql;
using ToDoList.Model;


namespace ToDoList.Data.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _config;
        private DBConnection _connectionString;
        
        public EmailRepository(IConfiguration config, DBConnection connectinString)
        {
            _config = config;
            _connectionString = connectinString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<AList> GetId()
        {
            var datePlusTwelve = DateTime.Now.AddHours(12);

            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM list
                        WHERE duedate < @datePlusTwelve
                        and sentmail = 'false'";


            return await db.QueryFirstOrDefaultAsync<AList>(sql, new { datePlusTwelve });
        }

        public async Task<User> GetEmail(string user)
        {
            var db = dbConnection();
        
            var sql = @"
                        SELECT *
                        FROM users
                        WHERE username = @User";                      

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { User = user });
                
        }

        public async Task<bool> UpdateState(int id)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE list
                        SET sentmail = 'true'
                        WHERE id = @id
                        ";

            var result = await db.ExecuteAsync(sql, new { id });
            return true;

        }

        public async Task<bool> SendEmail(string email, int id, string title, string description, DateTime dueDate)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("API ToDoList", _config.GetSection("EmailUser").Value));
            mimeMessage.To.Add(MailboxAddress.Parse(email));
            mimeMessage.Subject = _config.GetSection("EmailSubject").Value;

            BodyBuilder messageBody = new BodyBuilder();
            messageBody.HtmlBody = _config.GetSection("EmailBody").Value + "<b>" + $" The Item {title} - {description} expire on {dueDate}" + "</b>";

            mimeMessage.Body = messageBody.ToMessageBody();

            SmtpClient clientSmtp = new SmtpClient();

            try
            {
                clientSmtp.CheckCertificateRevocation = false;
                clientSmtp.Connect(_config.GetSection("EmailServer").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
                clientSmtp.Authenticate(_config.GetSection("EmailUser").Value, _config.GetSection("EmailPass").Value);
                clientSmtp.Send(mimeMessage);
                 
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                clientSmtp.Disconnect(true);
                clientSmtp.Dispose();
                 
                 var updated = await UpdateState(id);
                
            }
            return true;
        }
    }
}
