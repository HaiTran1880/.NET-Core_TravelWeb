using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TTTN_Travel.Models
{
    public partial class Kh
    {
        [DisplayName("Id")]
        public int Idkh { get; set; }
        [DisplayName("Họ tên")]
        public string Hoten { get; set; }
        [DisplayName("Số điện thoại")]
        public string Sdt { get; set; }
        [DisplayName("Địa chỉ")]
        public string Dc { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
