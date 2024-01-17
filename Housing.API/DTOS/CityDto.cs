using System.ComponentModel.DataAnnotations;

namespace Housing.API.DTOS
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="you must enter city name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "you must enter country name")]
        public string Country { get; set; }
    }
}
