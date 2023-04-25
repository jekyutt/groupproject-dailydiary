using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDiary.Model.Requests
{
    public class AddCommentaryRequest
    {
        public string Text { get; set; }
        public string OwnerId { get; set; }
        public int PostId { get; set; }
    }
}
