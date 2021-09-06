using System;
using System.ComponentModel.DataAnnotations;
namespace PropertyManager.Models
{
    public class Files 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OriginID { get; set; }

        [Required]
        public byte Type { get; set; } // Type represents usage [1 = Property Image]

        [Required]
        public string Url { get; set; } 

        public bool IsActive { get; set; } = true;
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}