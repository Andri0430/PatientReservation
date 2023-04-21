using Microsoft.EntityFrameworkCore;
using PatientReservation.Data;
using PatientReservation.Interface;
using PatientReservation.Models;

namespace PatientReservation.Repository
{
    public class KamarRepository : IKamarRepository
    {
        private readonly SqlContext _context;

        public KamarRepository(SqlContext context)
        {
            _context = context;
        }
        public ICollection<Kamar> GetAllKamar()
        {
            return _context.Kamar.Include(k => k.Perawatan).ToList();
        }

        public Kamar KamarGetById(int IdKamar)
        {
            return _context.Kamar.Where(k => k.Id == IdKamar).Include(p => p.Perawatan).FirstOrDefault();
        }
        public void CreateKamar(Kamar kamar)
        {
            _context.Kamar.Add(kamar);
            _context.SaveChanges();
        }

        public void DeleteKamar(int IdKamar)
        {
            _context.Kamar.Remove(KamarGetById(IdKamar));
            _context.SaveChanges();
        }
        public void UpdateKamar(Kamar kamar)
        {
            _context.Update(kamar);
            _context.SaveChanges();
        }
    }
}