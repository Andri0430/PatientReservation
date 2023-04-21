using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientReservation.Interface;
using PatientReservation.Dto;
using PatientReservation.Models;
using PatientReservation.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace ReservationPatient.Controllers
{
    public class PasienController : Controller
    {
        private readonly IPerawatanRepository _perawatanRepository;
        private readonly IKamarRepository _kamarRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly SqlContext _context;
        private readonly IWebHostEnvironment _env;
        public PasienController(IPerawatanRepository perawatanRepository, IKamarRepository kamarRepository, IPatientRepository patientRepository, SqlContext context, IWebHostEnvironment env)
        {
            _perawatanRepository = perawatanRepository;
            _kamarRepository = kamarRepository;
            _patientRepository = patientRepository;
            _context = context;
            _env = env;
        }

        private void ListJenisPerawatan()
        {
            ViewBag.JenisPerawatan = _perawatanRepository.GetAllPerawatan().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.NamaPerawatan
            });

            ViewBag.Status = _context.Status.ToList().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Stat
            });
        }

        [HttpGet]
        public IActionResult GetItemsByPerawatan(int idPerawatan)
        {
            var items = _kamarRepository.GetAllKamar().Where(k => k.Perawatan.Id == idPerawatan).ToList();
            return Json(items);
        }
        public IActionResult Index(int? perawatanId)
        {
            ListJenisPerawatan();
            return View(_patientRepository.GetAllPasien());
        }

        public IActionResult Create()
        {
            ListJenisPerawatan();

            ViewBag.TodayDate = DateTime.Now.ToString("yyyy/MM/dd").Trim();
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] PasienDto pasienDto, IFormFile Foto)
        {
            int cek = 0;
            var notelp = _patientRepository.GetAllPasien().Where(p => p.NoTelepon == pasienDto.NoTelepon).FirstOrDefault();
            var ktp = _patientRepository.GetAllPasien().Where(p => p.NoKtp == pasienDto.NoKtp).FirstOrDefault();

            var fileName = "Foto_" + pasienDto.Nama + Path.GetExtension(Foto.FileName);
            var filepath = Path.Combine(_env.WebRootPath, "upload", fileName);

            using (var stream = System.IO.File.Create(filepath))
            {
                Foto.CopyTo(stream);
            }


            if (pasienDto.Nama.Length < 3)
            {
                cek = 1;
                ModelState.AddModelError(nameof(pasienDto.Nama), "Nama Minimal 3 Digit");
            }
            if (pasienDto.TempatLahir.Length < 3)
            {
                cek = 1;
                ModelState.AddModelError(nameof(pasienDto.TempatLahir), "Nama Tempat Lahir Minimal 3 Digit");
            }
            if (notelp != null)
            {
                cek = 1;
                ModelState.AddModelError(nameof(pasienDto.NoTelepon), "Nomor Telepon Telah Digunakan");
                if (pasienDto.NoTelepon.Length >= 1 && pasienDto.NoTelepon.Length < 12)
                {
                    ModelState.AddModelError(nameof(pasienDto.NoTelepon), "Nomor Telepon Tidak Sesuai");
                }
            }
            if (ktp != null)
            {
                cek = 1;
                ModelState.AddModelError(nameof(pasienDto.NoKtp), "No.KTP Telah Digunakan");
                if (pasienDto.NoKtp.Length < 16)
                {
                    ModelState.AddModelError(nameof(pasienDto.NoKtp), "No.KTP Harus 16 Digit");
                }
            }
            if (pasienDto.alamatKota.Length < 3)
            {
                cek = 1;
                ModelState.AddModelError(nameof(Pasien.Alamat.Kota), "Nama Kota Minimal 3 Digit");
            }
            if (pasienDto.alamatJalan.Length < 3)
            {
                cek = 1;
                ModelState.AddModelError(nameof(Pasien.Alamat.Jalan), "Nama Jalan Minimal 3 Digit");
            }
            else
            {
                if (cek == 0)
                {
                    var newPasien = new Pasien
                    {
                        Nama = pasienDto.Nama,
                        Umur = pasienDto.Umur,
                        TempatLahir = pasienDto.TempatLahir,
                        TanggalLahir = pasienDto.TanggalLahir,
                        NoTelepon = pasienDto.NoTelepon,
                        NoKtp = pasienDto.NoKtp,
                        Foto = fileName,
                        Perawatan = _perawatanRepository.GetPerawatanById(pasienDto.idPerawatan),
                        Alamat = new Alamat
                        {
                            Kota = pasienDto.alamatKota,
                            Jalan = pasienDto.alamatJalan
                        },
                        TanggalMasuk = pasienDto.TanggalMasuk,
                        TanggalKeluar = pasienDto.TanggalKeluar,
                        Stat = _context.Status.Where(s => s.Id == 1).FirstOrDefault(),
                        KamarID = pasienDto.idKamar
                    };

                    if (pasienDto.idPerawatan == 3)
                    {
                        _patientRepository.CreatePasien(newPasien);
                        TempData["NotificationType"] = "success";
                        TempData["NotificationMessage"] = "Pasien Telah Berhasil Ditambahkan";
                        ModelState.Clear();
                    }
                    else
                    {
                        var kamarId = _kamarRepository.KamarGetById(pasienDto.idKamar);
                        kamarId.Terisi += 1;

                        if (kamarId.Terisi <= kamarId.Kuota)
                        {
                            _patientRepository.CreatePasien(newPasien);
                            _kamarRepository.UpdateKamar(kamarId);

                            TempData["NotificationType"] = "success";
                            TempData["NotificationMessage"] = "Pasien Telah Berhasil Ditambahkan";
                            ModelState.Clear();
                        }
                        else
                        {
                            TempData["NotificationType"] = "danger";
                            TempData["NotificationMessage"] = "Kamar Sudah Terisi Penuh !!!";
                        }
                    }
                }
            }
            ListJenisPerawatan();
            return View();
        }
        public IActionResult Delete(int id)
        {
            var getIdPasien = _patientRepository.GetPasienById(id);
            if (getIdPasien.KamarID == 0)
            {
                _patientRepository.DeletePasien(getIdPasien.Id);
            }
            else
            {
                var kamarId = _kamarRepository.KamarGetById(getIdPasien.KamarID);
                kamarId.Terisi -= 1;
                _kamarRepository.UpdateKamar(kamarId);
                _patientRepository.DeletePasien(getIdPasien.Id);
            }
            return RedirectToAction("Index", "Pasien");
        }

        public IActionResult Update(int id)
        {
            ListJenisPerawatan();
            var pasien = _patientRepository.GetPasienById(id);
            return View(pasien);
        }

        [HttpPost]
        public IActionResult Update([FromForm] PasienDto pasienDto, int id, IFormFile Foto, string alamatKota, string alamatJalan, int IdStatus)
        {
            int cek = 0;
            var pasienExist = _patientRepository.GetAllPasien().Where(p => p.NoKtp == pasienDto.NoKtp || p.NoTelepon == pasienDto.NoTelepon).FirstOrDefault();
            var pasienGetId = _patientRepository.GetPasienById(id);
            ListJenisPerawatan();

            var fileName = "Foto_" + pasienDto.Nama + Path.GetExtension(Foto.FileName);
            var filepath = Path.Combine(_env.WebRootPath, "upload", fileName);

            using (var stream = System.IO.File.Create(filepath))
            {
                Foto.CopyTo(stream);
            }

            if (pasienExist != null && pasienGetId.NoKtp != pasienDto.NoKtp)
            {
                cek = 1;
                TempData["NotificationType"] = "danger";
                TempData["NotificationMessage"] = "Pasien Sudah Pernah Terdaftar";
            }
            else
            {

                if (pasienGetId.KamarID != pasienDto.idKamar)
                {
                    var pindahKamar = _kamarRepository.KamarGetById(pasienDto.idKamar);
                    pindahKamar.Terisi += 1;
                    _kamarRepository.UpdateKamar(pindahKamar);

                    var kamarLama = _kamarRepository.KamarGetById(pasienGetId.KamarID);
                    kamarLama.Terisi -= 1;
                    _kamarRepository.UpdateKamar(kamarLama);
                }
                pasienGetId.Id = id;
                pasienGetId.Nama = pasienDto.Nama;
                pasienGetId.Umur = pasienDto.Umur;
                pasienGetId.TempatLahir = pasienDto.TempatLahir;
                pasienGetId.TanggalLahir = pasienDto.TanggalLahir;
                pasienGetId.NoTelepon = pasienDto.NoTelepon;
                pasienGetId.NoKtp = pasienDto.NoKtp;
                pasienGetId.Alamat.Kota = alamatKota;
                pasienGetId.Alamat.Jalan = alamatJalan;
                pasienGetId.Perawatan = _context.Perawatan.Where(p => p.Id == pasienDto.idPerawatan).FirstOrDefault();
                pasienGetId.KamarID = pasienDto.idKamar;
                pasienGetId.TanggalMasuk = pasienDto.TanggalMasuk;
                pasienGetId.TanggalKeluar = pasienDto.TanggalKeluar;
                pasienGetId.Foto = fileName;
                pasienGetId.Stat = _context.Status.Where(s => s.Id == IdStatus).FirstOrDefault();
                _patientRepository.UpdatePasien(pasienGetId);

                if (pasienGetId.Stat.Id == 2)
                {
                    var addSembuh = new Sembuh
                    {
                        Foto = fileName,
                        Nama = pasienDto.Nama,
                        Umur = pasienDto.Umur,
                        KotaAsal = pasienDto.alamatKota,
                        Perawatan = _context.Perawatan.Where(p => p.Id == pasienDto.idPerawatan).FirstOrDefault(),
                        Status = _context.Status.Where(s => s.Id == IdStatus).FirstOrDefault()
                    };
                    _context.Sembuh.Add(addSembuh);
                    Delete(id);
                }
            }

            if (cek == 0)
            {
                TempData["NotificationType"] = "success";
                TempData["NotificationMessage"] = "Data Pasien Sudah Di Update";
            }
            return View(pasienGetId);
        }

        public IActionResult Detail(int id)
        {
            var getPasienById = _patientRepository.GetPasienById(id);
            return View(getPasienById);
        }

        public IActionResult Sembuh()
        {
            var Sembuh = _context.Sembuh.Include(s => s.Perawatan).Include(s => s.Status).ToList();
            return View(Sembuh);
        }
    }
}