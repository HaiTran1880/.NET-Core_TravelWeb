using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTTN_Travel.Models
{
    public partial class Tour
    {
        [DisplayName("Tên tour")]
        public string Tentuor { get; set; }
        [DisplayName("Ảnh")]
        public string Image { get; set; }
        [DisplayName("Ngày bắt đầu")]
        public DateTime? Ngaybd { get; set; }
        [DisplayName("Ngày kết thúc")]
        public DateTime? Ngaykt { get; set; }
        [DisplayName("Mô tả")]
        public string Mota { get; set; }
        [DisplayName("Giá")]
        public string Gia { get; set; }
        [DisplayName("Quốc gia")]
        public string Quocgia { get; set; }
        [DisplayName("Trong nước")]
        public bool? Trongnuoc { get; set; }
        [DisplayName("Id")]
        public int Idtour { get; set; }
        [DisplayName("Lịch trình")]
        public string Lichtrinh { get; set; }
        [DisplayName("Đánh giá")]
        public int Danhgia { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }
    }

}
