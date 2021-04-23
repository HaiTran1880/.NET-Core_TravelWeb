using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTTN_Travel.Models
{
    public partial class Addmin
    {
        [DisplayName("Id")]
        public int Idad { get; set; }
        [DisplayName("User Name")]
        public string Username { get; set; }
        [DisplayName("PassWork")]
        public string Passwork { get; set; }
        [DisplayName("Image")]
        public string Image { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }

    }
}
