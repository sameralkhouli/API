using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inlamningsuppgift.Models.Entities
{
    public class UserEntity
    {
        public UserEntity(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public UserEntity(int id, string firstName, string lastName, string email, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Required, Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
    }
}
