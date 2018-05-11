using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Extensions.Validators;

namespace WebApp.Models
{
    [Table("Artist")]
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [ArtistNameValidator]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Members Count")]
        [Display(Name = "Members Count")]
        public int MembersCount { get; set; }

        public string Genre { get; set; }

        [Required(ErrorMessage = "Enter City Name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Sessions")]
        public ICollection<Session> Sessions { get; set; }

        public Artist() {}

        public Artist(string name, int members, string genre, string city)
        {
            Name = name;
            MembersCount = members;
            Genre = genre;
            City = city;
        }
    }
}
