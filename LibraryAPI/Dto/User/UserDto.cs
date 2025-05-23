﻿namespace LibraryAPI.Dto.User
{
    public class UserDto
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required DateTime ExpiresOn { get; set; }
    }
}
