using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Quality.Common;

namespace Quality.DataAccess
{
    public interface IImageDataAccess
    {
        void ResizeImage(string path);
        Task<Result> WriteFile(IFormFile file);
        Task<Result> WriteFileToPath(IFormFile file, string path);
    }
}