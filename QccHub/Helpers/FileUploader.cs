using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QccHub.Helpers
{
    public static class FileUploader
    {
        public static async Task<string> Upload(string directoryPath, IFormFile file) 
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(directoryPath, fileName);
            try
            {
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                
                if (File.Exists(filePath))
                    File.Delete(filePath);
                
                using (var fileStream = File.Create(filePath))
                {
                    await file.CopyToAsync(fileStream);
                }
                return fileName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void Delete(string filePath) 
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
