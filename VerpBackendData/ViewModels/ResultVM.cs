using System;
using System.Collections.Generic;
using System.Text;

namespace VerpBackendData.ViewModels
{
    public class ResultVM
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get; set; }
    }

    public class AuthLoginVM : ResultVM
    {
        public string Token { get; set; }
        public Models.User UserData { get; set; }
    }
}
