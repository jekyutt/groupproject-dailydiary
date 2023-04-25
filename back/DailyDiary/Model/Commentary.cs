using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DailyDiary.Model
{
    [Table("Commentaries")]
    public class Commentary
    {
        [Column("id")]
        public int Id { get; init; }
        [Column("text")]
        [Required]
        public string Text { get; init; }
        [Column("ownerId")]
        [Required]
        public string OwnerId { get; init; }
        [Column("postId")]
        [Required]
        public int PostId { get; init; }
        [Column("likes")]
        public ICollection<User> Likes { get; init; }
        
        public virtual User Owner { get; init; }
        public virtual Post Post { get; init; }

    }
}
