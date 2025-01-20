using CampusEventHubApi.Data;
using CampusEventHubApi.DTOs;
using CampusEventHubApi.Factories;
using CampusEventHubApi.Models;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services
{
    public class KalendarService : IKalendarService
    {
        private readonly ApplicationDbContext _context;
        private readonly List<IEventObserver> _observers = new(); // Lista observera

        public KalendarService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Registracija observera
        public void RegisterObserver(IEventObserver observer)
        {
            _observers.Add(observer);
        }

        // Obavijesti observere o kreiranju
        private void NotifyEventCreated(Kalendar kalendar)
        {
            foreach (var observer in _observers)
            {
                observer.OnEventCreated(kalendar);
            }
        }

        // Obavijesti observere o ažuriranju
        private void NotifyEventUpdated(Kalendar kalendar)
        {
            foreach (var observer in _observers)
            {
                observer.OnEventUpdated(kalendar);
            }
        }

        // Obavijesti observere o brisanju
        private void NotifyEventDeleted(int kalendarId)
        {
            foreach (var observer in _observers)
            {
                observer.OnEventDeleted(kalendarId);
            }
        }

        public async Task<IEnumerable<KalendarRequestDto>> GetAllAsync()
        {
            var kalendari = await _context.Kalendari.ToListAsync();

            // Koristi KalendarDtoFactory za mapiranje
            return kalendari.Select(KalendarDtoFactory.CreateRequestDto);
        }

        public async Task<KalendarRequestDto> GetByIdAsync(int id)
        {
            var kalendar = await _context.Kalendari.FindAsync(id);

            if (kalendar == null)
            {
                return null; // Događaj nije pronađen
            }

            // Koristi KalendarDtoFactory za mapiranje
            return KalendarDtoFactory.CreateRequestDto(kalendar);
        }



        //NOVO DODANO
        public async Task<IEnumerable<KalendarRequestDto>> GetByMonthAsync(int month, int year)
        {
            var kalendari = await _context.Kalendari
                .Where(k => k.Datum.Month == month && k.Datum.Year == year)
                .ToListAsync();

            return kalendari.Select(KalendarDtoFactory.CreateRequestDto);
        }

        public async Task<KalendarRequestDto> CreateAsync(KalendarRequestDto request)
        {
            // Validacija i parsiranje datuma
            if (!DateTime.TryParseExact(request.Datum, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                throw new FormatException("Neispravan format datuma. Koristite format dd.MM.yyyy.");
            }

            // Validacija i parsiranje vremena
            TimeSpan? parsedTime = null;
            if (!string.IsNullOrEmpty(request.Vrijeme))
            {
                if (!TimeSpan.TryParseExact(request.Vrijeme, @"hh\:mm", CultureInfo.InvariantCulture, out var time))
                {
                    throw new FormatException("Neispravan format vremena. Koristite format HH:mm.");
                }
                parsedTime = time;
            }

            var kalendar = new Kalendar
            {
                Naslov = request.Naslov,
                Opis = request.Opis,
                Datum = parsedDate,
                Vrijeme = parsedTime
            };

            _context.Kalendari.Add(kalendar);
            await _context.SaveChangesAsync();

            NotifyEventCreated(kalendar); // Obavijesti observere o kreiranju

            return KalendarDtoFactory.CreateRequestDto(kalendar);
        }

        public async Task<bool> UpdateAsync(int id, KalendarRequestDto request)
        {
            var kalendar = await _context.Kalendari.FindAsync(id);

            if (kalendar == null)
            {
                return false;
            }

            // Validacija i parsiranje datuma
            if (!DateTime.TryParseExact(request.Datum, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                throw new FormatException("Neispravan format datuma. Koristite format dd.MM.yyyy.");
            }

            // Validacija i parsiranje vremena
            TimeSpan? parsedTime = null;
            if (!string.IsNullOrEmpty(request.Vrijeme))
            {
                if (!TimeSpan.TryParseExact(request.Vrijeme, @"hh\:mm", CultureInfo.InvariantCulture, out var time))
                {
                    throw new FormatException("Neispravan format vremena. Koristite format HH:mm.");
                }
                parsedTime = time;
            }

            kalendar.Naslov = request.Naslov;
            kalendar.Opis = request.Opis;
            kalendar.Datum = parsedDate;
            kalendar.Vrijeme = parsedTime;

            _context.Entry(kalendar).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            NotifyEventUpdated(kalendar); // Obavijesti observere o ažuriranju

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var kalendar = await _context.Kalendari.FindAsync(id);

            if (kalendar == null)
            {
                return false;
            }

            _context.Kalendari.Remove(kalendar);
            await _context.SaveChangesAsync();

            NotifyEventDeleted(id); // Obavijesti observere o brisanju

            return true;
        }
    }
}
