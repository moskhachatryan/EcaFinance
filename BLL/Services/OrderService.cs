using BLL.Services.Interfaces;
using Common.Enums;
using Common.ViewModels;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly FinanceContext _context;

        public OrderService(FinanceContext context)
        {
            _context = context;
        }

        #region Category 
        public async Task AddCategoryAsync(CategoryViewModel model)
        {
            Category category = new Category()
            {
                Name = model.Name,
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        public async Task EditCategoryAsync(CategoryViewModel model)
        {
            var category = _context.Categories.Where(a => a.Id == model.Id).FirstOrDefault();

            category.Name = model.Name;

            await _context.SaveChangesAsync();
        }
        public async Task RemoveCategoryAsync(int id)
        {
            var category = await _context.Categories.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (category != null)
                category.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
        {
            var category = _context.Categories
                .Where(a => a.Id == id)
                .Select(a => new CategoryViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                }).FirstOrDefault();

            return category;
        }
        public async Task<IPagedList<CategoryViewModel>> GetAllCategoriesAsync(int page)
        {
            IPagedList<CategoryViewModel> categories = await _context.Categories.Where(a => !a.IsDeleted).Select(a => new CategoryViewModel
            {
                Id = a.Id,
                Name = a.Name,
            }).OrderBy(a => a.Name).ToPagedListAsync(page, 15);

            return categories;
        }
        public async Task<List<SelectListItem>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.Where(a => !a.IsDeleted).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,
            }).OrderBy(a => a.Text).ToListAsync();

            return categories;
        }
        #endregion

        #region ChurchService
        public async Task AddChurchServiceAsync(ChurchServiceViewModel model)
        {
            ChurchService churchService = new ChurchService()
            {
                Name = model.Name,
            };

            await _context.ChurchServices.AddAsync(churchService);
            await _context.SaveChangesAsync();
        }
        public async Task EditChurchServiceAsync(ChurchServiceViewModel model)
        {
            var churchService = _context.ChurchServices.Where(a => a.Id == model.Id).FirstOrDefault();

            churchService.Name = model.Name;

            await _context.SaveChangesAsync();
        }
        public async Task<IPagedList<ChurchServiceViewModel>> GetAllChurchServicesAsync(int page)
        {
            IPagedList<ChurchServiceViewModel> churchServices = _context.ChurchServices.Where(a => !a.IsDeleted).Select(a => new ChurchServiceViewModel
            {
                Id = a.Id,
                Name = a.Name,
            }).OrderByDescending(a => a.Name).ToPagedList(page, 15);

            return churchServices;
        }
        public async Task<List<SelectListItem>> GetAllChurchServicesAsync()
        {
            var churchServices = _context.ChurchServices.Where(a => !a.IsDeleted).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,
            }).OrderBy(a => a.Text).ToList();

            return churchServices;
        }
        public async Task<ChurchServiceViewModel> GetChurchServiceByIdAsync(int id)
        {
            var churchService = _context.ChurchServices
               .Where(a => a.Id == id)
               .Select(a => new ChurchServiceViewModel
               {
                   Id = a.Id,
                   Name = a.Name,
               }).FirstOrDefault();

            return churchService;
        }
        public async Task RemoveChurchServiceAsync(int id)
        {
            var churchService = await _context.ChurchServices.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (churchService != null)
                churchService.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Orders
        public async Task AddOrderAsync(OrderViewModel model)
        {
            Order order = new Order()
            {
                CreatedDate = DateTime.Now,
                OrderDate = model.OrderDate,
                Description = model.Description,
                Price = model.Price,
                Type = model.Type,
                OrderNumber = model.OrderNumber,
                CategoryId = model.CategoryId,
                ChurchServiceId = model.ChurchServiceId,
                Sort = model.Sort,
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        public async Task EditOrderAsync(OrderViewModel model)
        {
            var order = _context.Orders.Where(a => a.Id == model.Id).FirstOrDefault();

            order.OrderDate = model.OrderDate;
            order.OrderNumber = model.OrderNumber;
            order.CategoryId = model.CategoryId;
            order.Description = model.Description;
            order.ChurchServiceId = model.ChurchServiceId;
            order.Price = model.Price;
            order.Type = model.Type;
            order.Sort = model.Sort;

            await _context.SaveChangesAsync();
        }
        public async Task<(IPagedList<OrderViewModel>, int, int)> GetAllOrdersAsync(int page, OrderType? type, string? search, DateTime? startDate, DateTime? endDate, int? service, int? category)
        {
            var query = _context.Orders.Where(a => !a.IsDeleted
            && (string.IsNullOrEmpty(search) || a.OrderNumber.ToString().Contains(search))
            && (!startDate.HasValue || a.OrderDate > startDate.Value)
            && (!endDate.HasValue || a.OrderDate < endDate.Value)
            && (!endDate.HasValue || a.OrderDate < endDate.Value)
            && (!service.HasValue || a.ChurchServiceId == service.Value)
            && (!category.HasValue || a.CategoryId == category.Value)
            && (!type.HasValue || type.Value == a.Type))
                .Select(a => new OrderViewModel
                {
                    Id = a.Id,
                    OrderNumber = a.OrderNumber,
                    CategoryId = a.CategoryId,
                    Description = a.Description,
                    Price = a.Price,
                    Type = a.Type,
                    ChurchServiceId = a.ChurchServiceId,
                    CreatedDate = a.CreatedDate,
                    OrderDate = a.OrderDate,
                    Sort = a.Sort,
                    Category = new CategoryViewModel()
                    {
                        Name = a.Category.Name
                    },
                    ChurchService = new ChurchServiceViewModel()
                    {
                        Name = a.ChurchService.Name
                    }
                });

            var allDebitAmount = query.Where(a => a.Type == OrderType.Debit).Sum(a => a.Price);
            var allDepositAmount = query.Where(a => a.Type == OrderType.Deposit).Sum(a => a.Price);

            var orders = query.OrderByDescending(a => a.Sort).ThenByDescending(a => a.OrderNumber).ToPagedList(page, 20);

            return (orders, (int)allDebitAmount, (int)allDepositAmount);
        }
        public async Task<OrderViewModel> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Where(a => a.Id == id)
                .Select(a => new OrderViewModel
                {
                    Id = a.Id,
                    OrderNumber = a.OrderNumber,
                    OrderDate = a.OrderDate,
                    CreatedDate = a.CreatedDate,
                    Price = a.Price,
                    Type = a.Type,
                    Description = a.Description,
                    CategoryId = a.CategoryId,
                    ChurchServiceId = a.ChurchServiceId,
                    Sort = a.Sort
                }).FirstOrDefaultAsync();

            return order;
        }
        public async Task RemoveOrderAsync(int id)
        {
            var order = await _context.Orders.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (order != null)
                order.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Price
        public async Task UpdatePresentAmount() {
            var price = await _context.Prices.Where(a => a.Id == 1).FirstOrDefaultAsync();

            var orders = await _context.Orders.Where(a => !a.IsDeleted).ToListAsync();

            var allDebitAmount = orders.Where(a=> a.Type == OrderType.Debit).Sum(a => a.Price);
            var allDepositAmount = orders.Where(a=> a.Type == OrderType.Deposit).Sum(a => a.Price);

            price.PresentAmount = price.InitialAmount - allDebitAmount + allDepositAmount;
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetPresentBalance() => _context.Prices.Where(a => a.Id == 1).FirstOrDefault().PresentAmount;
        #endregion

        #region BalanceLimit
        public async Task AddBalanceLimitAsync(BalanceLimitViewModel model)
        {
            BalanceLimit balanceLimit = new BalanceLimit()
            {
                Limit = model.Limit,
                CategoryId = model.CategoryId,
                ChurchServiceId = model.ChurchServiceId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
            };

            await _context.BalanceLimits.AddAsync(balanceLimit);
            await _context.SaveChangesAsync();
        }

        public async Task EditBalanceLimitAsync(BalanceLimitViewModel model)
        {
            var balanceLimit = _context.BalanceLimits.Where(a => a.Id == model.Id).FirstOrDefault();

            balanceLimit.Limit = model.Limit;
            balanceLimit.StartDate = model.StartDate;
            balanceLimit.EndDate = model.EndDate;
            balanceLimit.CategoryId = model.CategoryId;
            balanceLimit.ChurchServiceId = model.ChurchServiceId;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveBalanceLimitAsync(int id)
        {
            var balacneLimit = await _context.BalanceLimits.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (balacneLimit != null)
                balacneLimit.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task<BalanceLimitViewModel> GetBalanceLimitByIdAsync(int id)
        {
            var balanceLimit = _context.BalanceLimits
                .Where(a => a.Id == id)
                .Select(a => new BalanceLimitViewModel
                {
                    Id = a.Id,
                    ChurchServiceId = a.ChurchServiceId,
                    CategoryId = a.CategoryId,
                    EndDate = a.EndDate,
                    StartDate = a.StartDate,
                    Limit = a.Limit
                }).FirstOrDefault();

            return balanceLimit;
        }

        public async Task<IPagedList<BalanceLimitViewModel>> GetAllBalanceLimitAsync(int page)
        {
            var total = await (from b in _context.BalanceLimits
                               join c in _context.Categories on b.CategoryId equals c.Id
                               join o in _context.Orders on b.CategoryId equals o.CategoryId into gj
                         from o in gj.DefaultIfEmpty()
                         where !b.IsDeleted && o.OrderDate <= b.EndDate && o.OrderDate >= b.StartDate
                         group o by new { b.CategoryId, b.Limit, b.EndDate, b.StartDate, b.Id, CategoryName = c.Name } into g
                         select new BalanceLimitViewModel
                         {
                             Id = g.Key.Id,
                             Limit = g.Key.Limit,
                             EndDate = g.Key.EndDate,
                             StartDate = g.Key.StartDate,
                             SpentMoney = g.Sum(a => a.Price),
                             CategoryId = g.Key.CategoryId,
                             CategoryName = g.Key.CategoryName,
                             Percent = (int)g.Sum(a => a.Price) * 100 / (int)g.Key.Limit
                         }).OrderBy(a => a.Id).ToPagedListAsync(page, 15);

            return total;
        }
        #endregion
    }
}
