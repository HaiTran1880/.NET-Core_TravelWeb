using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TTTN_Travel.Models
{
    public class User
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Họ tên")]
        public string Name { get; set; }
        [DisplayName("Ảnh")]
        public string Image { get; set; }
        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }
        [DisplayName("Mật khẩu")]
        public string Pass { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [DisplayName("Địa chỉ")]
        public string Adress { get; set; }
        [DisplayName("Chứng minh/CCCD")]
        public string Cmt { get; set; }
        [DisplayName("Giớ tính")]
        public bool Gt { get; set; }
        

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
