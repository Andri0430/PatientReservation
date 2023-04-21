using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientReservation.Models
{
    public class Alamat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        [StringLength(30,MinimumLength = 3,ErrorMessage = "Panjang Karakter Diantara 3 dan 30 Huruf")]
        public string Kota { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Panjang Karakter Diantara 3 dan 30 Huruf")]
        public string Jalan { get; set; }
    }
}