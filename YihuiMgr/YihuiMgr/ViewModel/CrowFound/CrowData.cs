using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YihuiMgr.ViewModel.CrowFound
{
    public class CrowData
    {
        public int Num { get; set; }
        public string Category { get; set; }

        public int Id { get; set; }

        public string ActorName { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public decimal? FundEnd { get; set; }

        public DateTime? CreatTime { get; set; }

        public string FrontImg { get; set; }

        public string Title { get; set; }
    }
}
