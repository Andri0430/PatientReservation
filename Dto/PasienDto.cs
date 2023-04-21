namespace PatientReservation.Dto
{
    public class PasienDto
    {
        public string Nama { get; set; }
        public int Umur { get; set; }
        public string TempatLahir { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string NoTelepon { get; set; }
        public string NoKtp { get; set; }
        public string? Foto { get; set; }
        public int idPerawatan { get; set; }
        public int idKamar { get; set; }
        public string alamatKota { get; set; }
        public string alamatJalan { get; set; }
        public DateTime TanggalMasuk { get; set; }
        public DateTime TanggalKeluar { get; set; }
    }
}