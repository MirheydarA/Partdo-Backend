using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Utilities.Abstract
{
    public interface IFileService
    {
        string Upload(IFormFile file);
        void Delete(string photoName);
        bool IsImage(IFormFile file);
        bool IsBiggerThanSize(IFormFile file, int size = 100);
    }
}
