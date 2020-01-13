using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YihuiMgr.ViewModel.Top
{
    public class TopDataModel
    {
        public int Id { get; set; }

        public int SourceId { get; set; }

        public string SourceType { get; set; }

        /// <summary>
        /// 1、艺人 2、众筹 3视频
        /// </summary>
        public int SourceTypeInt { get; set; }

        public string ImgaeUrl { get; set; }

        public DateTime Date { get; set; }
        public int OrderIndex { get; set; }

        public string Title { get; set; }
    }
}
