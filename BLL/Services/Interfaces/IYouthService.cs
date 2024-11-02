using Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BLL.Services.Interfaces
{
    public interface IYouthService
    {
        Task AddYouthMemberAsync(YouthMembersViewModel model);
        Task EditYouthMemberAsync(YouthMembersViewModel model);
        Task RemoveYouthMemberAsync(int id);
        Task<YouthMembersViewModel> GetYouthMemberByIdAsync(int id);
        Task<List<YouthMembersViewModel>> GetYouthMembersAsync();
    }
}
