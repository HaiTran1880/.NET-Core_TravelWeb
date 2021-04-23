using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TTTN_Travel.Models.Global
{
    public class UploadFiles : Controller
    {
        public static string ImgURL(IFormFile img)
        {
            //---
            //lay ten file
            string _fileName = img.FileName;
            //lay thoi gian gan vao ten file
            //var timestamp = DateTime.Now.ToFileTime();
            //_fileName = timestamp + "_" + _fileName;
            //lay duong dan cua file
            //var currentPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Upload/a");
            var currentPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images");

            if (Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }

            string _path = Path.Combine(currentPath, _fileName);
            //upload file
            using (var stream = new FileStream(_path, FileMode.Create))
            {
                img.CopyTo(stream);
            }

            return _path;
        }

        public static void RemoveImgURL(string _path)//, string folder
        {
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _path)))
            {
                //xoa anh
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _path));
            }
        }
    }
}
