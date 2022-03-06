using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VerpBackendData.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string LastName { get; set; }
        public string Firstname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVerified { get; set; }
        public string Pin { get; set; }
        public bool IsAgreed { get; set; }
        public string AuthenticationToken { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
