using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientReservation.Dto;
using PatientReservation.Interface;
using PatientReservation.Models;

namespace PatientReservation.Controllers
{
    public class KamarController : Controller
    {
        private readonly IKamarRepository _kamarRepository;
        private readonly IPerawatanRepository _perawatanRepository;
        private readonly IPatientRepository _patientRepository;
        public KamarController(IKamarRepository kamarRepository, IPerawatanRepository perawatanRepository, IPatientRepository patientRepository)
        {
            _kamarRepository = kamarRepository;
            _perawatanRepository = perawatanRepository;
            _patientRepository = patientRepository;
        }

        private void ListKamarByIdPerawatanNoInap()
        {
            ViewBag.JenisPerawatan = _perawatanRepository.GetAllPerawatan().Where(p => p.NamaPerawatan != "Rawat Jalan").Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.NamaPerawatan.ToString()
            });
        }

        public IActionResult Index(int? idJenisPerawatan)
        {
            ListKamarByIdPerawatanNoInap();
            var kamar = _kamarRepository.GetAllKamar();
            if (idJenisPerawatan != null)
            {
                kamar = _kamarRepository.GetAllKamar().Where(x => x.Perawatan.Id == idJenisPerawatan).ToList();
                if (kamar.Count == 0)
                {
                    TempData["NotificationType"] = "danger";
                    TempData["NotificationMessage"] = "Tidak Ada Data!!!";
                }
            }
            return View(kamar);
        }
        public IActionResult Create()
        {
            ListKamarByIdPerawatanNoInap();
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] KamarDto kamarDto, int idPerawatan)
        {
            ListKamarByIdPerawatanNoInap();
            var cekNamaKamar = _kamarRepository.GetAllKamar()
                                .Where(k => k.NamaKamar.Trim().ToLower() == kamarDto.NamaKamar.Trim().ToLower()).FirstOrDefault();

            if (cekNamaKamar != null)
            {
                TempData["NotificationType"] = "danger";
                TempData["NotificationMessage"] = "Kamar Sudah Pernah Ditambahkan!!";
            }
            else
            {
                if (idPerawatan == 1 && kamarDto.Kuota > 5)
                {
                    TempData["NotificationType"] = "danger";
                    TempData["NotificationMessage"] = "Kuota Kamar IGD Tidak Cukup";
                }
                else if (idPerawatan == 2 && kamarDto.Kuota > 10)
                {
                    TempData["NotificationType"] = "danger";
                    TempData["NotificationMessage"] = "Kuota Kamar Rawat Inap Tidak Cukup";
                }
                else
                {
                    var kamarTamp = new Kamar
                    {
                        NamaKamar = kamarDto.NamaKamar,
                        Perawatan = _perawatanRepository.GetPerawatanById(idPerawatan),
                        Kuota = kamarDto.Kuota
                    };

                    _kamarRepository.CreateKamar(kamarTamp);
                    ModelState.Clear();
                    TempData["NotificationType"] = "success";
                    TempData["NotificationMessage"] = "Kamar Berhasil Ditambahkan";
                }
            }
            return View();
        }
        public IActionResult Update(int id)
        {
            ListKamarByIdPerawatanNoInap();
            var kamarId = _kamarRepository.KamarGetById(id);

            if (kamarId == null)
            {
                return NotFound();
            }
            return View(kamarId);
        }

        [HttpPost]
        public IActionResult Update(int id, [Bind("Id", "NamaKamar", "JenisKamar", "Kuota")] KamarDto kamarDto, int idperawatan)
        {
            ListKamarByIdPerawatanNoInap();

            var kamarExist = _kamarRepository.GetAllKamar().FirstOrDefault(k => k.NamaKamar == kamarDto.NamaKamar);
            var kamar = _kamarRepository.KamarGetById(id);
            var perawatan = _perawatanRepository.GetPerawatanById(idperawatan);
            if (kamar == null)
            {
                return NotFound();
            }
            else
            {
                kamar.Kuota = kamarDto.Kuota;
                if (kamarExist != null && kamar.NamaKamar != kamarDto.NamaKamar)
                {
                    kamar.NamaKamar = kamarDto.NamaKamar;
                    TempData["NotificationType"] = "danger";
                    TempData["NotificationMessage"] = "Kamar Telah Tersedia";
                    ModelState.AddModelError(nameof(kamar.NamaKamar), "");
                }
                else if (idperawatan == 1 && kamar.Kuota > 5)
                {
                    TempData["NotificationType"] = "danger";
                    TempData["NotificationMessage"] = "Kuota Kamar IGD Tidak Cukup";
                    ModelState.AddModelError(nameof(kamar.Kuota), "");
                }
                else if (idperawatan == 2 && kamar.Kuota > 10)
                {
                    TempData["NotificationType"] = "danger";
                    TempData["NotificationMessage"] = "Kuota Kamar Rawat Inap Tidak Cukup";
                    ModelState.AddModelError(nameof(kamar.Kuota), "");
                }
                else
                {
                    TempData["NotificationType"] = "success";
                    TempData["NotificationMessage"] = "Kamar Berhasil Di Update";

                    kamar.NamaKamar = kamarDto.NamaKamar;
                    kamar.Perawatan = perawatan;
                    _kamarRepository.UpdateKamar(kamar);
                }
            }
            return View(kamar);
        }
        public IActionResult Delete(int id)
        {
            var kamarId = _kamarRepository.KamarGetById(id);

            if (kamarId == null)
            {
                return NotFound();
            }
            else
            {
                if (kamarId.Terisi > 0)
                {
                    TempData["NotificationType"] = "danger";
                    TempData["NotificationMessage"] = "Kamar Tidak Bisa DIhapus!!!";
                }
                else
                {
                    TempData["NotificationType"] = "success";
                    TempData["NotificationMessage"] = "Kamar Berhasil Dihapus!!!";
                    _kamarRepository.DeleteKamar(id);
                }
            }
            return RedirectToAction("Index", "Kamar");
        }

        public IActionResult Show(int id)
        {
            var getPasienByKamar = _patientRepository.GetAllPasien().Where(p => p.KamarID == id).ToList();
            return View(getPasienByKamar);
        }
    }
}