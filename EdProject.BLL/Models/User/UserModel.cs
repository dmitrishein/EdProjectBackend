﻿namespace EdProject.BLL.Models.User
{
    public class UserModel : BaseModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
