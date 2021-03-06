using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PropertyManager.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte PropertyType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
