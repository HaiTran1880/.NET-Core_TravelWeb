using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TTTN_Travel.Models
{
    public partial class Tout
    {
            [DisplayName("Id")]
            public int Id { get; set; }
            [DisplayName("Tên tour")]
            public string Ten { get; set; }
            [DisplayName("Ảnh")]
            public string Image { get; set; }
            [DisplayName("Ngày bắt đầu")]
            public DateTime? Ngay { get; set; }
            [DisplayName("Mô tả")]
            public string Mota { get; set; }
            [DisplayName("Giá")]
            public string Giakm { get; set; }
            [DisplayName("Chi tiết")]
            public string Chitiet { get; set; }
            [DisplayName("Lịch trình")]
            public string Lichtrinh { get; set; }
            [DisplayName("Đánh giá")]
            public int Rate { get; set; }
            [DisplayName("Giá trẻ em")]
            public string Giatre { get; set; }
            [NotMapped]
            [DisplayName("Upload Image")]
            public IFormFile ImageFile { get; set; }
    }
}
