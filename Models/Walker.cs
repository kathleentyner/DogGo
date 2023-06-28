using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }
        [StringLength(25, MinimumLength = 6)]
        [DisplayName("link to a picture")]
       
        public string ImageUrl { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}