using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDePoliticasDeCompliance.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}