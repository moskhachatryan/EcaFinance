using BLL.Services.Interfaces;
using Common.ViewModels;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class YouthService : IYouthService
    {

        private readonly FinanceContext _context;

        public YouthService(FinanceContext context)
        {
            _context = context;
        }

        public async Task AddYouthMemberAsync(YouthMembersViewModel model)
        {

            YouthMembers youthMember = new YouthMembers()
            {
                FullName = model.FullName,
                AbsenceScore = model.AbsenceScore,
                GameScore = model.GameScore,
                IntelectualScore = model.IntelectualScore,
                ChatScore = model.ChatScore,
                Img = model.Img,
                LastWeek = model.LastWeek    
            };

            await _context.YouthMembers.AddAsync(youthMember);
            await _context.SaveChangesAsync();
        }

        public async Task EditYouthMemberAsync(YouthMembersViewModel model)
        {
            var youthMember = _context.YouthMembers.Where(a => a.Id == model.Id).FirstOrDefault();

            if(youthMember != null) { 
                youthMember.FullName = model.FullName;
                youthMember.AbsenceScore = model.AbsenceScore;
                youthMember.ChatScore = model.ChatScore;
                youthMember.Img = model.Img;
                youthMember.IntelectualScore = model.IntelectualScore;
                youthMember.GameScore = model.GameScore;
                youthMember.LastWeek = model.LastWeek;
            }

            await _context.SaveChangesAsync();
        }

        public Task<YouthMembersViewModel> GetYouthMemberByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<YouthMembersViewModel>> GetYouthMembersAsync()
        {
            var youthMembers = await _context.YouthMembers.Select(a => new YouthMembersViewModel
            {
                FullName = a.FullName,
                LastWeek = a.LastWeek,
                GameScore = a.GameScore,
                IntelectualScore = a.IntelectualScore,
                ChatScore = a.ChatScore,
                Img = a.Img,
                Id = a.Id,
                AbsenceScore = a.AbsenceScore,
                TotalScore = (a.AbsenceScore + a.GameScore + a.IntelectualScore + a.ChatScore),
            }).OrderBy(a => a.FullName).ToListAsync();

            return youthMembers;
        }

        public Task RemoveYouthMemberAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
