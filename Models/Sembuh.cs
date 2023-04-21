using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientReservation.Models
{
    public class Sembuh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string? Foto { get; set; }
        public string Nama { get; set; }
        public int Umur { get; set; }
        public string KotaAsal { get; set; }
        public Perawatan Perawatan { get; set; }
        public Status Status { get; set; }
    }
}
