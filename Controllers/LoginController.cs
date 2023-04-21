using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PatientReservation.Data;
using PatientReservation.Dto;
using System.Security.Claims;

namespace PatientReservation.Controllers
{
    public class LoginController : Controller
    {
        private readonly SqlContext _context;
        public LoginController(SqlContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] AdminDto data)
        {
            var admin = _context.Admin.Where(x => x.Username == data.UsernameDto && x.Password == data.PasswordDto).FirstOrDefault();

            if (admin != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim("username",admin.Username),
                    new Claim("role","admin")
                };
                var identity = new ClaimsIdentity(claims, "Cookie", "name", "role");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return Redirect("/Home/Index");
            }
            else
            {
                TempData["Message"] = "Anda Gagal Login!!!";
                return View();
            }
        }
    }
}