using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DailyDiary.Model.Responses
{
    public class GetUserResponse
    {
        public string Id { get; set; }
        public User.UserType Type { get; init; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public DateTime BirthDate { get; init; }
        public string PhoneNumber { get; set; }
        public string Bio { get; init; }
        public bool Adult { get; init; }
        public string AvatarLink { get; init; }

        [JsonIgnore] public virtual ICollection<Post> UsersPosts { get; set; }
        [JsonIgnore] public virtual ICollection<Commentary> UsersCommentaries { get; set; }
        [JsonIgnore] public virtual ICollection<User> Followings { get; set; }
        [JsonIgnore] public virtual ICollection<User> Followers { get; set; }


    }
}
