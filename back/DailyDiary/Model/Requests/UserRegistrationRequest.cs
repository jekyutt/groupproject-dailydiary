using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDiary.Model.Requests
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }
        public string Bio { get; set; }
        public string AvatarLink { get; set; }
    }
}
