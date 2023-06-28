using DogGo.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography.Xml;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Make sure your pup has a name!")]
        [MaxLength(35)]
        public string Name { get; set; }

        
        [Required(ErrorMessage ="Don't know the breed, just put All American")]
        public string Breed { get; set; }
        public string Notes { get; set; }

        [StringLength(25, MinimumLength = 6)]
        [DisplayName("link to a picture")]
        public string ImageUrl { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
