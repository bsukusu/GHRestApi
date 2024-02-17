using System.ComponentModel.DataAnnotations;

namespace GoRestApi.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez..")]
        [StringLength(30, ErrorMessage = "Kullanıcı adı maksimim otuz karakter olmalı.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez..")]
        [MinLength(6)]
        [MaxLength(16)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez..")]
        [MinLength(6)]
        [MaxLength(16)]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}
