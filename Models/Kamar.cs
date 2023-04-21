using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientReservation.Models
{
    public class Kamar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Panjang Karakter Diantara 3 dan 20 Huruf")]
        public string NamaKamar { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Panjang Karakter Diantara 3 dan 20 Huruf")]
        public Perawatan Perawatan { get; set; }
        [Required]
        public int Terisi { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 1)]
        public int Kuota { get; set; }
    }
}