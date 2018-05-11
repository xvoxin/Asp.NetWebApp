using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    [Table("Session")]
    public class Session
    {
        public int SessionID { get; set; }

        [Display(Name = "Artist")]
        [Required(ErrorMessage = "Choose Artist")]
        public int ArtistID { get; set; }

        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Choose Employee")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Enter Session Length")]
        [Display(Name = "Session Length")]
        public int SessionLength { get; set; }

        [Required(ErrorMessage = "Enter Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Session Date")]
        public DateTime SessionDate { get; set; }

        public Artist Artist { get; set; }
        public Employee Employee { get; set; }

    }
}
