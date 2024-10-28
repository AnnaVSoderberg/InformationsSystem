using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Information_System_ASP.Net.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [ForeignKey("Driver")]
        public int DriverID { get; set; }

        [Required]
        [MaxLength(500)]
        public string EventDescription { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string LoggedByEmployee { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BeloppUt { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BeloppIn { get; set; }

        // Navigation property to the associated Driver
        public Driver Driver { get; set; }
    }
}
