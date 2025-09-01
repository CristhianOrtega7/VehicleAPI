using System.ComponentModel.DataAnnotations;

namespace VehicleApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; } 

        [Required]
        [StringLength(50)]
        public string Make { get; set; } = default!;   

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = default!;  

        [Range(1900, 2100)]
        public int Year { get; set; }

        [StringLength(30)]
        public string? Color { get; set; }

        [StringLength(20)]
        public string? Vin { get; set; } 
    }
}