using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YihuiMgr.ViewModel.Actor
{
    public class ActorModel
    {
        public int Num { get; set; }

        public int Id { get; set; }

        public string ActorName { get; set; }

        public string ActorCareer { get; set; }

        public int? ActorGender { get; set; }

        public string ActorContact { get; set; }

        public DateTime? BirthDay { get; set; }

        public string ActorTags { get; set; }

        public DateTime JoinDate { get; set; }

        public string FrontIcon { get; set; }
    }
}
