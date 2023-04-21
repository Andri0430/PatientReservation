using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientReservation.Interface;

namespace PatientReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPerawatanRepository _perawatanRepository;
        public HomeController(IPerawatanRepository perawatanRepository)
        {
            _perawatanRepository = perawatanRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
