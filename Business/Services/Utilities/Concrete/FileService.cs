using Business.Services.Utilities.Abstract;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Services.Utilities.Concrete
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)

        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string Upload(IFormFile file)
        {
            var fileName = Guid.NewGuid() + "_" + file.FileName;
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Users/assets/images", fileName);

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
 


        public void Delete(string photoName)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Users/assets/images", photoName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        public bool IsBiggerThanSize(IFormFile file, int size = 2000)
        {
            if (file.Length / 1024 < size) return true;

            return false;
        }

        public bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image/")) return true;

            return false;
        }

       
    }
}
