using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientReservation.Models
{
    public class Perawatan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        public string NamaPerawatan { get; set; }
        ICollection<Pasien> Pasien { get; set; }
        ICollection<Kamar> Kamar { get; set; }
    }
}