using VoyageVault.Components.Models;

namespace VoyageVault.Components.Services
{
    public class VoyageService : IVoyageService
    {
        private readonly HttpClient _httpClient;
        public VoyageService(HttpClient httpClient) 
        {
            this._httpClient = httpClient;
        }
        public Task<TripRequest> GetTripRequest(TripRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
