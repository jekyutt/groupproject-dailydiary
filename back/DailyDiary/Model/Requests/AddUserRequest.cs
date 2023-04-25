using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDiary.Model.Requests
{
    public class AddUserRequest
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public DateTime BirthDate { get; init; }
        public string PhoneNumber { get; init; }
        public string Bio { get; init; }
        public string AvatarLink { get; init; }
    }
}
