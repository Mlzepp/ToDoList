using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Model
{
    public class User
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }    
        public DateTime Created { get; set; }
        public string Email { get; set; }
    }
}
