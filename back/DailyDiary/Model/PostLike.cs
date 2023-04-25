using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DailyDiary.Model
{
    public class PostLike
    {
        public int PostId { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual Post Post { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
