using System;
using System.Collections.Generic;
using System.Text;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CultureValue { get; set; }
        public int ChannelId { get; set; }
        public bool HasTranslations { get; set; }
    }
}
