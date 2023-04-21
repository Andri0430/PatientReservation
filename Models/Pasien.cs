using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientReservation.Models
{
    public class Pasien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Panjang Karakter Diantara 3 dan 30 Huruf")]
        public string? Nama { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        [Range(1, 99, ErrorMessage = "Umur harus antara 1 dan 99")]
        [StringLength(2, MinimumLength = 1)]
        public int Umur { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Panjang Karakter Diantara 3 dan 20 Huruf")]
        public string TempatLahir { get; set; }

        [Required]
        public DateTime TanggalLahir { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        [StringLength(11, MinimumLength = 11)]
        public string NoTelepon { get; set; }

        [Required(ErrorMessage = "Harap Isi Bidang Ini")]
        [StringLength(16,MinimumLength = 16,ErrorMessage ="No.KTP Tidak Sesuati")]
        public string NoKtp { get; set; }
        public string Foto { get; set; }
        public Alamat Alamat { get; set; }
        [Required]
        public Perawatan Perawatan { get; set; }

        [ForeignKey("Kamar")]

        [Required]
        public int KamarID { get; set; }
        ICollection<Kamar> Kamars { get; set; }

        [Required]
        public DateTime TanggalMasuk { get; set; }
        [Required]
        public DateTime TanggalKeluar { get; set; }
        public Status Stat { get; set; }
    }
}