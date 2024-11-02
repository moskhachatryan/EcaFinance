using BLL.Services.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcaFinance.Controllers
{
    public class YouthController : Controller
    {
        private readonly IYouthService _youthService;

        public YouthController(IYouthService youthService)
        {
            _youthService = youthService;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _youthService.GetYouthMembersAsync();

            return View(model);
        }
    }
}
