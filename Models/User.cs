using System;
using System.ComponentModel.DataAnnotations;

namespace PropertyManager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
