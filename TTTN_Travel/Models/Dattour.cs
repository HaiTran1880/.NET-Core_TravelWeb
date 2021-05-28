using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TTTN_Travel.Models
{
    public partial class Dattour
    {
        [DisplayName("Họ tên")]
        public string Hoten { get; set; }
        [DisplayName("Tên tour")]
        public string Tentuor { get; set; }
        [DisplayName("Số điện thoại")]
        public string Sdt { get; set; }
        [DisplayName("Địa chỉ")]
        public string Dc { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime Date { get; set; }
        [DisplayName("Số người lớn")]
        public int Songuoi { get; set; }
        
        [DisplayName("Ghi chú")]
        public string Ghichu { get; set; }
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Trẻ em")]
        public int Treem { get; set; }
        [DisplayName("Thành tiền")]
        public string Thanhtien { get; set; }
        [DisplayName("Trạng thái")]
        public string Trangthai { get; set; }

    }
}
