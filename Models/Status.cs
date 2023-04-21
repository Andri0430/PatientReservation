using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientReservation.Models
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        public string Stat { get; set; }
        public ICollection<Pasien> Pasiens { get; set; }
    }
}