﻿namespace e_commerce_API.Models
{
    public class AuthModel
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Email { get; set; }
        public List<string> Role {  get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }  
    }
}
