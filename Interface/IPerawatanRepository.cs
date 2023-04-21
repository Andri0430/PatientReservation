using PatientReservation.Models;

namespace PatientReservation.Interface
{
    public interface IPerawatanRepository
    {
        ICollection<Perawatan> GetAllPerawatan();
        Perawatan GetPerawatanById(int id);
        void CreatePerawatan(Perawatan perawatan);
        void DeletePerawatan(int IdPerawatan);
        void UpdatePerawatan(Perawatan perawatan);
    }
}
