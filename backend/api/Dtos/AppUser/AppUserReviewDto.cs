using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.AppUser
{
    public class AppUserReviewDto
    {
        public string UserId { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Avatar { get; set; }
    }
}