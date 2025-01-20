using CampusEventHubApi.DTOs;
using CampusEventHubApi.Models;

namespace CampusEventHubApi.Services.Interfaces
{
    public interface IKalendarService
    {
        Task<IEnumerable<KalendarRequestDto>> GetAllAsync();
        Task<KalendarRequestDto> GetByIdAsync(int id);
        Task<IEnumerable<KalendarRequestDto>> GetByMonthAsync(int month, int year);
        Task<KalendarRequestDto> CreateAsync(KalendarRequestDto request);
        Task<bool> UpdateAsync(int id, KalendarRequestDto request);
        Task<bool> DeleteAsync(int id);

        
        void RegisterObserver(IEventObserver observer);
    }
}
