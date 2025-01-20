using CampusEventHubApi.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services.Interfaces
{
    public interface IOcjenaService
    {
        Task<IEnumerable<OcjenaResponseDto>> GetAllAsync();
        Task<OcjenaResponseDto> GetByIdAsync(int id);
        Task<OcjenaResponseDto> CreateAsync(OcjenaRequestDto request);
        Task<bool> UpdateAsync(int id, OcjenaUpdateDto request); // Koristi OcjenaUpdateDto
        Task<bool> DeleteAsync(int id);
    }
}
