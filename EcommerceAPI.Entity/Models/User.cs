using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.Models
{
    public class User
    {
        [Column("UserId")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for this field is 60 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for this field is 60 characters.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Firstname is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for this field is 60 characters.")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for this field is 60 characters.")]
        public string? Lastname { get; set; }

        [Required(ErrorMessage = "DateOfBirth is a required field.")]
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }
        public string? Address { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }
        public int? Phone { get; set; }
        public bool IsActive { get; set; }
        public bool Status { get; set; }



    }
}
