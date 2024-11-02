using Common.ViewModels;
using DataAccess.Data;
using DataAccess.Entities;
using EcaFinance.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcaFinance.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly FinanceContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(FinanceContext context,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task EditAsync(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            user.UserName = model.Email;
            user.NormalizedUserName = model.Email.ToUpper();

            _context.SaveChanges();

        }

        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            var user = await _context.Users.Where(a => a.Id == id).Select(a => new UserViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
            }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<UserViewModel> GetByEmailAsync(string email)
        {
            var user = await _context.Users.Where(a => a.Email == email).Select(a => new UserViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
            }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
            var users = await _context.Users.Where(a => !a.Deleted).Select(a => new UserViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                Deleted = a.Deleted,
            }).OrderBy(a => a.FirstName).ToListAsync();

            return users;
        }

        public async Task RemoveAsync(int id)
        {
            var user = await _context.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user != null)
                user.Deleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task ChangeUserDetailsAsync(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            user.UserName = model.Email;
            user.NormalizedUserName = model.Email.ToUpper();

            _context.SaveChanges();

        }

    }
}
