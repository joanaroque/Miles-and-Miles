using CinelAirMiles.Prism.Helpers;
using CinelAirMiles.Prism.Models;
using System.Threading.Tasks;

namespace CinelAirMiles.Prism.Services
{
    public interface IApiService
    {
        Task<Response<UserResponse>> GetClientByEmailAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string email);


        Task<bool> CheckConnection(string url);


        Task<Response<TokenResponse>> GetTokenAsync(
              string urlBase,
              string servicePrefix,
              string controller,
              TokenRequest request);


        Task<Response<object>> RegisterUserAsync(
           string urlBase,
           string servicePrefix,
           string controller,
           UserResponse userRequest);

        Task<Response<object>> RecoverPasswordAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            EmailRequest emailRequest);

        Task<Response<object>> PutAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model,
            string tokenType,
            string accessToken);

        Task<Response<object>> ChangePasswordAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            ChangePasswordRequest changePasswordRequest,
            string tokenType,
            string accessToken);

        Task<Response<object>> GetListAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken);

        Task<Response<object>> PostAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model,
            string tokenType,
            string accessToken);

        Task<Response<object>> DeleteAsync(
           string urlBase,
           string servicePrefix,
           string controller,
           int id,
           string tokenType,
           string accessToken);
    }
}
