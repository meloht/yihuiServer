using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YihuiMgr.ViewModel.Actor
{
    public class UserInfo
    {
        public string HeadImg { get; set; }

        public string ShowVideo { get; set; }

        public string Phone { get; set; }

        public int CityId { get; set; }

        public string Alias { get; set; }

        public string Career { get; set; }

        public DateTime BirthDay { get; set; }

        public int Gender { get; set; }

        public string FrontIcon { get; set; }

        public string Intro { get; set; }

        public List<string> Tags { get; set; }
    }
}
