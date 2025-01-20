using CampusEventHubApi.DTOs;
using CampusEventHubApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services.Decorators
{
    public class ValidationDecorator : IKalendarService
    {
        private readonly IKalendarService _innerService;

        public ValidationDecorator(IKalendarService innerService)
        {
            _innerService = innerService;
        }

        public async Task<KalendarRequestDto> CreateAsync(KalendarRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Naslov))
            {
                throw new ArgumentException("Naslov je obavezan.");
            }

            if (!DateTime.TryParseExact(request.Datum, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                throw new FormatException("Neispravan format datuma. Koristite format dd.MM.yyyy.");
            }

            return await _innerService.CreateAsync(request);
        }

        public async Task<bool> UpdateAsync(int id, KalendarRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Naslov))
            {
                throw new ArgumentException("Naslov je obavezan.");
            }

            if (!DateTime.TryParseExact(request.Datum, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                throw new FormatException("Neispravan format datuma. Koristite format dd.MM.yyyy.");
            }

            return await _innerService.UpdateAsync(id, request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _innerService.DeleteAsync(id);
        }

        public async Task<KalendarRequestDto> GetByIdAsync(int id)
        {
            return await _innerService.GetByIdAsync(id);
        }

        public async Task<IEnumerable<KalendarRequestDto>> GetAllAsync()
        {
            return await _innerService.GetAllAsync();
        }

        public async Task<IEnumerable<KalendarRequestDto>> GetByMonthAsync(int month, int year)
        {
            // No specific validation needed for the month and year in this case, but you can add it if required.
            return await _innerService.GetByMonthAsync(month, year);
        }

        // Implementacija metode za registraciju observera
        public void RegisterObserver(IEventObserver observer)
        {
            _innerService.RegisterObserver(observer);
        }
    }
}
