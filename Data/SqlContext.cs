using Microsoft.EntityFrameworkCore;
using PatientReservation.Models;

namespace PatientReservation.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Alamat> Alamat { get; set; }
        public DbSet<Kamar> Kamar { get; set; }
        public DbSet<Perawatan> Perawatan { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Pasien> Pasien { get; set; }
        public DbSet<Sembuh> Sembuh { get; set; }
    }
}
