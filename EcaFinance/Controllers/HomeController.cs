using BLL.Services.Interfaces;
using Common.Enums;
using Common.ViewModels;
using DataAccess.Entities;
using EcaFinance.Extensions;
using EcaFinance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace EcaFinance.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            
            ViewData["Title"] = "Balance Limits";
            var balanceLimit = await _orderService.GetAllBalanceLimitAsync(page ?? 1);
            return View(balanceLimit);
        }

        #region Order
        public async Task<IActionResult> Finance(string? search, OrderType? type, DateTime? startDate, DateTime? endDate, int? page, int? service, int? category)
        {
            ViewData["Title"] = "Finance";
            ViewBag.Type = type;
            ViewBag.StartDate = startDate;
            ViewBag.EndtDate = endDate;
            ViewBag.Search = search;
            ViewBag.Categories = await _orderService.GetAllCategoriesAsync();
            ViewBag.ChurchServices = await _orderService.GetAllChurchServicesAsync();
            ViewBag.Page = page;

            var orders = await _orderService.GetAllOrdersAsync(page ?? 1, type, search, startDate, endDate, service, category);
            ViewBag.DebitAmount = orders.Item2.ToString("#,#");
            ViewBag.DepositAmount = orders.Item3.ToString("#,#");


            return Request.IsAjaxRequest()
                ? PartialView("_OrderList", orders.Item1)
                : View(orders.Item1);
        }

        public async Task<IActionResult> ManageOrder(int? id)
        {
            ViewBag.Categories = await _orderService.GetAllCategoriesAsync();
            ViewBag.ChurchServices = await _orderService.GetAllChurchServicesAsync();
            if (id.HasValue)
            {
                var orders = await _orderService.GetOrderByIdAsync(id.Value);

                return PartialView("_ManageOrder", orders);
            }

            return PartialView("_ManageOrder");
        }

        [HttpPost]
        public async Task<IActionResult> ManageOrder(OrderViewModel model)
        {
            if (model.Id == 0)
            {
                await _orderService.AddOrderAsync(model);
            }
            else
            {
                await _orderService.EditOrderAsync(model);
            }
            await _orderService.UpdatePresentAmount();

            return RedirectToAction("Finance");
        }

        public IActionResult DeleteOrder()
        {
            return PartialView("_DeleteOrder");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.RemoveOrderAsync(id);

            return RedirectToAction("Finance");
        }
        #endregion

        #region Category
        public async Task<IActionResult> Category(int? page)
        {
            ViewData["Title"] = "Categories";
            var categories = await _orderService.GetAllCategoriesAsync(page ?? 1);
            return View(categories);
        }

        public async Task<IActionResult> ManageCategory(int? id)
        {
            if (id.HasValue)
            {
                var events = await _orderService.GetCategoryByIdAsync(id.Value);

                return PartialView("_ManageCategory", events);
            }

            return PartialView("_ManageCategory");
        }

        [HttpPost]
        public async Task<IActionResult> ManageCategory(CategoryViewModel model)
        {
            if (model.Id == 0)
            {
                await _orderService.AddCategoryAsync(model);
            }
            else
            {
                await _orderService.EditCategoryAsync(model);
            }

            return RedirectToAction("Category");
        }

        public IActionResult DeleteCategory()
        {
            return PartialView("_DeleteCategory");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _orderService.RemoveCategoryAsync(id);

            return RedirectToAction("Category");
        }
        #endregion

        #region ChurchService
        public async Task<IActionResult> ChurchService(int? page)
        {
            ViewData["Title"] = "Services";
            var churchService = await _orderService.GetAllChurchServicesAsync(page ?? 1);
            return View(churchService);
        }


        public async Task<IActionResult> ManageChurchService(int? id)
        {
            if (id.HasValue)
            {
                var churchService = await _orderService.GetChurchServiceByIdAsync(id.Value);

                return PartialView("_ManageChurchServices", churchService);
            }

            return PartialView("_ManageChurchServices");
        }

        [HttpPost]
        public async Task<IActionResult> ManageChurchService(ChurchServiceViewModel model)
        {
            if (model.Id == 0)
            {
                await _orderService.AddChurchServiceAsync(model);
            }
            else
            {
                await _orderService.EditChurchServiceAsync(model);
            }

            return RedirectToAction("ChurchService");
        }

        public IActionResult DeleteChurchService()
        {
            return PartialView("_DeleteChurchService");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChurchService(int id)
        {
            await _orderService.RemoveChurchServiceAsync(id);

            return RedirectToAction("ChurchService");
        }
        #endregion

        #region BalanceLimit
        public async Task<IActionResult> ManageBalanceLimit(int? id)
        {
            ViewBag.Categories = await _orderService.GetAllCategoriesAsync();
            ViewBag.ChurchServices = await _orderService.GetAllChurchServicesAsync();

            if (id.HasValue)
            {
                var balanceLimit = await _orderService.GetBalanceLimitByIdAsync(id.Value);

                return PartialView("_ManageBalanceLimit", balanceLimit);
            }

            return PartialView("_ManageBalanceLimit");
        }

        [HttpPost]
        public async Task<IActionResult> ManageBalanceLimit(BalanceLimitViewModel model)
        {
            if (model.Id == 0)
            {
                await _orderService.AddBalanceLimitAsync(model);
            }
            else
            {
                await _orderService.EditBalanceLimitAsync(model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteBalanceLimit()
        {
            return PartialView("_DeleteBalanceLimit");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBalanceLimit(int id)
        {
            await _orderService.RemoveBalanceLimitAsync(id);

            return RedirectToAction("Index");
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetBalance()
        {
            var presentBalance = await _orderService.GetPresentBalance();
            return Content($"Առկա գումար: {((int)presentBalance).ToString("#,#")}դրամ");
        }
    }
}