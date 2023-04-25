using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDiary.Model.Requests
{
    public class PutPostRequest
    {
        [Required]
        public Post.PostType Type { get; init; }
        [Required]
        public string Title { get; init; }
        [Required]
        public string Text { get; init; }
        public DateTime ReleaseDate { get; init; }
        public bool NSFW { get; init; }
        public string ImageLink { get; init; }
        [Required]
        public string OwnerId { get; init; }
    }
}
