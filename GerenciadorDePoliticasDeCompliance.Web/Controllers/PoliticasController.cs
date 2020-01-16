using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDePoliticasDeCompliance.Controllers
{
    public class PoliticasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}