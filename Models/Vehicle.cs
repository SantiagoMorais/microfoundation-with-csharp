using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microfundamento_8_desenvolvimento_web_back_end.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The plate is required")]
        public string Plate { get; set; }

        [Display(Name = "Fabrication Year")]
        [Required(ErrorMessage = "The fabrication year is required")]
        public int FabricationYear { get; set; }

        [Display(Name = "Model Year")]
        [Required(ErrorMessage = "The model year is required")]
        public int ModelYear { get; set; }

        public ICollection<Consume> Consumes { get; set; }

    }
}