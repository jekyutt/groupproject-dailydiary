using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using System.Text.Json.Serialization;

namespace DailyDiary.Model
{
    public class User : IdentityUser
    {
        [Key] public override string Id { get; set; }
        [Column("type")] public UserType Type { get; set; }
        [Column("username")] [Required] public override string UserName { get; set; }
        [Column("passwordHash")] /*[Required]*/ public override string PasswordHash { get; set; }
        [Column("email")] [Required] public override string Email { get; set; }
        [Column("firstname")] [Required] public string Firstname { get; set; }
        [Column("lastname")] [Required] public string Lastname { get; set; }
        [Column("birthDate")] [Required] public DateTime BirthDate { get; init; }
        [Column("phoneNumber")] public override string PhoneNumber { get; set; }
        [Column("bio")] public string Bio { get; set; }
        [Column("adult")] public bool Adult { get; init; }
        [Column("avatarLink")] public string AvatarLink { get; set; }

        [JsonIgnore] public virtual ICollection<Post> UsersPosts { get; set; }
        [JsonIgnore] public virtual ICollection<Commentary> UsersCommentaries { get; set; }
        [JsonIgnore] public virtual ICollection<PostLike> PostLikes { get; set; }
        [JsonIgnore] public virtual ICollection<User> Followings { get; set; }
        [JsonIgnore] public virtual ICollection<User> Followers { get; set; }



        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum UserType
        {
            Private = 0,
            Public = 1
        }

        [JsonIgnore] public override string NormalizedUserName { get; set; }
        [JsonIgnore] public override string NormalizedEmail { get; set; }
        [NotMapped] [JsonIgnore] public override bool EmailConfirmed { get; set; }
        [NotMapped] [JsonIgnore] public override bool PhoneNumberConfirmed { get; set; }
        [NotMapped] [JsonIgnore] public override bool TwoFactorEnabled { get; set; }
        [NotMapped] [JsonIgnore] public override DateTimeOffset? LockoutEnd { get; set; }
        [NotMapped] [JsonIgnore] public override bool LockoutEnabled { get; set; }
        [NotMapped] [JsonIgnore] public override int AccessFailedCount { get; set; }
    }
}
