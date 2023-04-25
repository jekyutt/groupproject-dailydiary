using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DailyDiary.Model
{
    [Table("Posts")]
    public record Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; init; }
        [Column("type")]
        [Required]
        public PostType Type { get; init; }
        [Column("title")]
        [Required]
        public string Title { get; init; }
        [Column("text")]
        [Required]
        public string Text { get; init; }
        [Column("release_date")]
        public DateTime ReleaseDate { get; init; }
        [Column("nsfw")]
        public bool NSFW { get; init; }
        [Column("imageLink")]
        public string ImageLink { get; init; }
        [Column("ownerId")]
        public string OwnerId { get; init; }
        [Column("likes")]
        public ICollection<User> Likes { get; init; }

        public virtual User Owner { get; set; }
        public virtual ICollection<Commentary> Commentaries { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum PostType
        {
            Private = 0,
            Public = 1
        }
    }
}
