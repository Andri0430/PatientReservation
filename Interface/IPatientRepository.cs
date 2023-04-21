using PatientReservation.Models;

namespace PatientReservation.Interface
{
    public interface IPatientRepository
    {
        ICollection<Pasien> GetAllPasien();
        Pasien GetPasienById(int id);
        void CreatePasien(Pasien pasien);
        void DeletePasien(int IdPasien);
        void UpdatePasien(Pasien pasien);
    }
}
