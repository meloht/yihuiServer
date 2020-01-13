using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YihuiMgr.ViewModel.Actor
{
    public class UserRequstModel
    {
        public int Num { get; set; }
        public int? UserUid { get; set; }

        public string UserNick { get; set; }

        public string Career { get; set; }
        public int? Gender { get; set; }
        public string Tel { get; set; }
        public string Org { get; set; }

        public string Experience { get; set; }

        public DateTime? RequstTime { get; set; }
    }
}
