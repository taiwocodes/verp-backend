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
        public string Othername { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string AuthenticationToken { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
