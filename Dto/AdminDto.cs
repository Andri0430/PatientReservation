using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientReservation.Dto
{
    public class AdminDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }
        public string UsernameDto { get; set; }
        public string PasswordDto { get; set; }
    }
}
