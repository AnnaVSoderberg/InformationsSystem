using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Information_System_ASP.Net.Models
{
    public class Driver
    {
        [Key]
        public int DriverID { get; set; }

        [Required]
        [MaxLength(100)]
        public string DriverName { get; set; }

        [Required]
        [MaxLength(20)]
        public string CarReg { get; set; }

        [Required]
        public DateTime NoteDate { get; set; }

        [MaxLength(500)]
        public string NoteDescription { get; set; }

        [Required]
        public string ResponsibleEmployee { get; set; }

        // Navigation property to associated events
        public ICollection<Event> Events { get; set; }

        // Calculated properties for total amounts
        [NotMapped]
        public decimal TotalBeloppIn => Events?.Sum(e => e.BeloppIn) ?? 0;

        [NotMapped]
        public decimal TotalBeloppUt => Events?.Sum(e => e.BeloppUt) ?? 0;
    }
}
