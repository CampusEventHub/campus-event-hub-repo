using CampusEventHubApi.Data;
using CampusEventHubApi.DTOs;
using CampusEventHubApi.Factories;
using CampusEventHubApi.Models;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services
{
    public class OcjenaService : IOcjenaService
    {
        private readonly ApplicationDbContext _context;

        public OcjenaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OcjenaResponseDto>> GetAllAsync()
        {
            var ocjene = await _context.Ocjene
                .Include(o => o.Event) // Povezivanje s Event entitetom
                .Include(o => o.User) // Povezivanje s User entitetom
                .ToListAsync();

            // Koristimo Factory za kreiranje Response DTO-a
            return ocjene.Select(OcjenaDtoFactory.CreateResponseDto);
        }

        public async Task<OcjenaResponseDto> GetByIdAsync(int id)
        {
            var ocjena = await _context.Ocjene
                .Include(o => o.Event)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OcjenaID == id);

            if (ocjena == null) return null;

            // Koristimo Factory za kreiranje Response DTO-a
            return OcjenaDtoFactory.CreateResponseDto(ocjena);
        }

        public async Task<OcjenaResponseDto> CreateAsync(OcjenaRequestDto request)
        {
            // Provjera postoji li događaj
            var eventExists = await _context.Event.AnyAsync(e => e.IDEvent == request.EventID);
            if (!eventExists)
            {
                throw new ArgumentException($"Događaj s ID-jem {request.EventID} ne postoji.");
            }

            // Provjera postoji li korisnik
            var userExists = await _context.User.AnyAsync(u => u.IDUser == request.UserID);
            if (!userExists)
            {
                throw new ArgumentException($"Korisnik s ID-jem {request.UserID} ne postoji.");
            }

            var ocjena = new Ocjena
            {
                EventID = request.EventID,
                UserID = request.UserID,
                OcjenaVrijednost = request.OcjenaVrijednost,
                Komentar = request.Komentar
            };

            _context.Ocjene.Add(ocjena);
            await _context.SaveChangesAsync();

            // Koristi NotificationManager za obavijest o kreiranju
            NotificationManager.Instance.Notify($"Kreirana je nova ocjena za događaj {ocjena.EventID}.");

            // Koristimo Factory za kreiranje Response DTO-a
            return OcjenaDtoFactory.CreateResponseDto(ocjena);
        }

        public async Task<bool> UpdateAsync(int id, OcjenaUpdateDto request)
        {
            var ocjena = await _context.Ocjene.FindAsync(id);

            if (ocjena == null) return false;

            // Ažuriraj samo promjenjive vrijednosti
            ocjena.OcjenaVrijednost = request.OcjenaVrijednost;
            ocjena.Komentar = request.Komentar;

            _context.Entry(ocjena).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Koristi NotificationManager za obavijest o ažuriranju
            NotificationManager.Instance.Notify($"Ocjena s ID-jem {id} je ažurirana.");

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ocjena = await _context.Ocjene.FindAsync(id);

            if (ocjena == null) return false;

            _context.Ocjene.Remove(ocjena);
            await _context.SaveChangesAsync();

            // Koristi NotificationManager za obavijest o brisanju
            NotificationManager.Instance.Notify($"Ocjena s ID-jem {id} je obrisana.");

            return true;
        }
    }
}
