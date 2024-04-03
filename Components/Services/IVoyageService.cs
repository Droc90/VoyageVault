using VoyageVault.Components.Models;
namespace VoyageVault.Components.Services
{
    public interface IVoyageService
    {
        Task<TripRequest> GetTripRequest(TripRequest request);
    }
}
