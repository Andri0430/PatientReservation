using PatientReservation.Models;

namespace PatientReservation.Interface
{
    public interface IKamarRepository
    {
        ICollection<Kamar> GetAllKamar();
        Kamar KamarGetById(int IdKamar);
        void CreateKamar(Kamar kamar);
        void UpdateKamar(Kamar kamar);
        void DeleteKamar(int IdKamar);
    }
}