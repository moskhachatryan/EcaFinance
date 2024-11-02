using System;

namespace Common.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Deleted { get; set; }
        public string Password { get; set; }
    }
}
