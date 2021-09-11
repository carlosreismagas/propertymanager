using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PropertyManager.Models;

namespace PropertyManager.DTOs
{
    public class PropertyDTO
    {
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

        public List<Files> Files { get; set; }
    }
}