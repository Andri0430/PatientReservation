using PatientReservation.Data;
using PatientReservation.Interface;
using PatientReservation.Models;

namespace PatientReservation.Repository
{
    public class PerawatanRepository : IPerawatanRepository
    {
        private readonly SqlContext _context;
        public PerawatanRepository(SqlContext context)
        {
            _context = context;
        }
        public ICollection<Perawatan> GetAllPerawatan()
        {
            return _context.Perawatan.ToList();     
        }

        public Perawatan GetPerawatanById(int id)
        {
            return _context.Perawatan.Where(p => p.Id == id).FirstOrDefault();
        }
        public void CreatePerawatan(Perawatan perawatan)
        {
            _context.Perawatan.Add(perawatan);
            _context.SaveChanges();
        }

        public void DeletePerawatan(int IdPerawatan)
        {
            _context.Perawatan.Remove(GetPerawatanById(IdPerawatan));
            _context.SaveChanges();
        }

        public void UpdatePerawatan(Perawatan perawatan)
        {
            _context.Perawatan.Update(perawatan);
            _context.SaveChanges();
        }
    }
}
