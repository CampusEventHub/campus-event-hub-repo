using CampusEventHubApi.DTOs;
using CampusEventHubApi.Services.Interfaces;
using System;

namespace CampusEventHubApi.Services.Decorators
{
    public class OcjenaValidationDecorator : IOcjenaService
    {
        private readonly IOcjenaService _innerService;

        public OcjenaValidationDecorator(IOcjenaService innerService)
        {
            _innerService = innerService;
        }

        public async Task<IEnumerable<OcjenaResponseDto>> GetAllAsync()
        {
            return await _innerService.GetAllAsync();
        }

        public async Task<OcjenaResponseDto> GetByIdAsync(int id)
        {
            return await _innerService.GetByIdAsync(id);
        }

        public async Task<OcjenaResponseDto> CreateAsync(OcjenaRequestDto request)
        {
            // Validacija ocjene
            if (request.OcjenaVrijednost < 1 || request.OcjenaVrijednost > 5)
            {
                throw new ArgumentException("Ocjena mora biti između 1 i 5.");
            }

            // Ako je ocjena manja ili jednaka 2, komentar je obavezan
            if (request.OcjenaVrijednost <= 2 && string.IsNullOrEmpty(request.Komentar))
            {
                throw new ArgumentException("Komentar je obavezan za ocjene manje ili jednake 2.");
            }

            return await _innerService.CreateAsync(request);
        }

        public async Task<bool> UpdateAsync(int id, OcjenaUpdateDto request)
        {
            // Validacija ocjene
            if (request.OcjenaVrijednost < 1 || request.OcjenaVrijednost > 5)
            {
                throw new ArgumentException("Ocjena mora biti između 1 i 5.");
            }

            // Ako je ocjena manja ili jednaka 2, komentar je obavezan
            if (request.OcjenaVrijednost <= 2 && string.IsNullOrEmpty(request.Komentar))
            {
                throw new ArgumentException("Komentar je obavezan za ocjene manje ili jednake 2.");
            }

            return await _innerService.UpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _innerService.DeleteAsync(id);
        }
    }
}
