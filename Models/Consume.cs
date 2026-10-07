using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microfundamento_8_desenvolvimento_web_back_end.Models
{
    [Table("Consumes")]
    public class Consume
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Value is required")]
        public float Value { get; set; }

        [Required(ErrorMessage = "Milage is required")]
        public float Milage { get; set; }

        [Required(ErrorMessage = "FuelType is required")]
        [Display(Name = "Fuel Type")]
        public FuelType FuelType { get; set; }

        [Required(ErrorMessage = "The Vehicle Id is required")]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
    }

    public enum FuelType
    {
        Gasoline,
        Alcohol,
        Diesel,
        Electric
    }
}