using GoRestApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GoRestApi.Models
{
    public class UserViewModel
    {
        public class UserModel
        {
            public Guid Id { get; set; }
            public string Username { get; set; }

            public string Gender { get; set; }
            public string Status { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }

        }

        public class CreateUserModel
        {
            [Required(ErrorMessage = "Username is required.")]
            [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
            public string Username { get; set; }

            public string Gender { get; set; }
            public string Status { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }


        }

        public class EditUserModel
        {
            public Guid Id { get; set; }

            [Required(ErrorMessage = "Username is required.")]
            [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
            public string Username { get; set; }

            public string Gender { get; set; }
            public string Status { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }


    }
}