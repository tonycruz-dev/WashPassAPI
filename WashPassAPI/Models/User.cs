﻿using Microsoft.AspNetCore.Identity;

namespace WashPassAPI.Models;

public class User : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
    public string? Location { get; set; }
    // nav properties
    //public ICollection<ActivityAttendee> Activities { get; set; } = [];
    //public ICollection<Photo> Photos { get; set; } = [];
    //public ICollection<UserFollowing> Followings { get; set; } = [];
    //public ICollection<UserFollowing> Followers { get; set; } = [];
}
