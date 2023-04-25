using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDiary.Model.Requests
{
    public class AddPostRequest
    {
        public Post.PostType Type { get; init; }
        public string Title { get; init; }
        public string Text { get; init; }
        public DateTime ReleaseDate { get; init; }
        public bool NSFW { get; init; }
        public string ImageLink { get; init; }
        public string OwnerId { get; init; }
    }
}
