using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TTTN_Travel.Models
{
    public partial class Tintuc
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Tiêu đề")]
        public string Tentintuc { get; set; }
        [DisplayName("Ảnh")]
        public string Anhtintuc { get; set; }
        [DisplayName("Tóm tắt")]
        public string Tomtat { get; set; }
        [DisplayName("Chi tiết")]
        public string Chitiet { get; set; }
        [DisplayName("Ngày đăng")]
        public DateTime? Date { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
