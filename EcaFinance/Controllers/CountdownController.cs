using Microsoft.AspNetCore.Mvc;

namespace EcaFinance.Controllers
{
    public class CountdownController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
