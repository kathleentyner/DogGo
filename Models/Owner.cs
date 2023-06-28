using DogGo.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography.Xml;

namespace DogGo.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add a Name...")]
        [MaxLength(35)]
        public string Name { get; set; }


        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(55, MinimumLength = 5)]
        public string Address { get; set; }

        [Phone]

        public string Phone { get; set; }

        [Required]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }

        public List<Dog> dogs { get; set; }
        public Neighborhood Neighborhood { get; set; }

    }
}
