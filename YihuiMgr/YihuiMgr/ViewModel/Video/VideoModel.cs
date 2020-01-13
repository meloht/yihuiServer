using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YihuiMgr.ViewModel.Video
{
    public class VideoModel
    {
        public int Num { get; set; }

        public string ArtType { get; set; }

        public string VideoTitle { get; set; }

        public string VideoDuration { get; set; }

        public string VideoBrief { get; set; }

        public string FrontIcon { get; set; }

        public string Tags { get; set; }

        public DateTime CreateDate { get; set; }

        public int Id { get; set; }
    }
}
