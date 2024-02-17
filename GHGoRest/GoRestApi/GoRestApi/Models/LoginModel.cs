using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace GoRestApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez..")]
        [StringLength(30, ErrorMessage = "Kullanıcı adı maksimim otuz karakter olmalı.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez..")]
        [MinLength(6)]
        [MaxLength(16)]
        public string Password { get; set; }
    }

}

