using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

namespace MilesBackOffice.Web.Helpers
{
    public interface IImageHelper
    {
        /// <summary>
        /// receives an image and a string, saves the image inside the folder
        /// </summary>
        /// <param name="imagefile">imagefile</param>
        /// <param name="folder">folder</param>
        /// <returns>the image inside the folder</returns>
        Task<string> UploadImageAsync(IFormFile imagefile, string folder);
    }
}
