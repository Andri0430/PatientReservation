using Microsoft.EntityFrameworkCore;
using PatientReservation.Data;
using PatientReservation.Interface;
using PatientReservation.Models;

namespace PatientReservation.Repository
{
    public class PasienRepository : IPatientRepository
    {
        private readonly SqlContext _context;
        public PasienRepository(SqlContext context)
        {
            _context = context;
        }
        public ICollection<Pasien> GetAllPasien()
        {
            return _context.Pasien.Include(p => p.Alamat).Include(p => p.Perawatan).Include(p => p.Stat).ToList();
        }
        public Pasien GetPasienById(int id)
        {
            return _context.Pasien.Where(p => p.Id == id).Include(p => p.Perawatan).Include(p => p.Alamat).Include(p => p.Stat).FirstOrDefault();
        }
        public void CreatePasien(Pasien pasien)
        {
            _context.Pasien.Add(pasien);
            _context.SaveChanges();
        }
        public void DeletePasien(int IdPasien)
        {
            _context.Pasien.Remove(GetPasienById(IdPasien));
            _context.SaveChanges();
        }
        public void UpdatePasien(Pasien pasien)
        {
            _context.Pasien.Update(pasien);
            _context.SaveChanges();
        }
    }
}