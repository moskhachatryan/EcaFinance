using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class YouthMembersViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Img { get; set; }
        public int GameScore { get; set; }
        public int IntelectualScore { get; set; }
        public int AbsenceScore { get; set; }
        public int ChatScore { get; set; }
        public int LastWeek { get; set; }
        public int TotalScore { get; set; }

    }
}
