namespace CinelAirMilesLibrary.Common.Helpers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;


    public interface IBlobHelper
    {
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);


        Task<Guid> UploadBlobAsync(byte[] file, string containerName);


        Task<Guid> UploadBlobAsync(string image, string containerName);
    }
}
