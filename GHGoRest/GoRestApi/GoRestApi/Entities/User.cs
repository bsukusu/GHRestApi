using System.ComponentModel.DataAnnotations;

namespace GoRestApi.Entities
{

    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string NameSurname { get; set; } = null;

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }

}