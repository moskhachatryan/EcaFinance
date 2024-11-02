using Common.Enums;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace BLL.Services.Interfaces
{
    public interface IOrderService
    {
        #region Category
        Task AddCategoryAsync(CategoryViewModel model);
        Task EditCategoryAsync(CategoryViewModel model);
        Task RemoveCategoryAsync(int id);
        Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        Task<IPagedList<CategoryViewModel>> GetAllCategoriesAsync(int page);
        Task<List<SelectListItem>> GetAllCategoriesAsync();
        #endregion

        #region ChurchService
        Task AddChurchServiceAsync(ChurchServiceViewModel model);
        Task EditChurchServiceAsync(ChurchServiceViewModel model);
        Task RemoveChurchServiceAsync(int id);
        Task<ChurchServiceViewModel> GetChurchServiceByIdAsync(int id);
        Task<IPagedList<ChurchServiceViewModel>> GetAllChurchServicesAsync(int page);
        Task<List<SelectListItem>> GetAllChurchServicesAsync();
        #endregion

        #region Order
        Task AddOrderAsync(OrderViewModel model);
        Task EditOrderAsync(OrderViewModel model);
        Task RemoveOrderAsync(int id);
        Task<OrderViewModel> GetOrderByIdAsync(int id);
        Task<(IPagedList<OrderViewModel>, int, int)> GetAllOrdersAsync(int page, OrderType? type, string? search, DateTime? startDate, DateTime? endDate, int? service, int? category);
        #endregion

        #region Price
        Task UpdatePresentAmount();
        Task<decimal> GetPresentBalance();
        #endregion

        #region BalanceLimit
        Task AddBalanceLimitAsync(BalanceLimitViewModel model);
        Task EditBalanceLimitAsync(BalanceLimitViewModel model);
        Task RemoveBalanceLimitAsync(int id);
        Task<BalanceLimitViewModel> GetBalanceLimitByIdAsync(int id);
        Task<IPagedList<BalanceLimitViewModel>> GetAllBalanceLimitAsync(int page);
        #endregion
    }
}
