using CampusEventHubApi.Models;
using CampusEventHubApi.Services.Interfaces;
using System;

namespace CampusEventHubApi.Services.Observers
{
    public class EmailNotifier : IEventObserver
    {
        public void OnEventCreated(Kalendar kalendar)
        {
            Console.WriteLine($"[Email] Novi događaj '{kalendar.Naslov}' kreiran.");
        }

        public void OnEventUpdated(Kalendar kalendar)
        {
            Console.WriteLine($"[Email] Događaj '{kalendar.Naslov}' ažuriran.");
        }

        public void OnEventDeleted(int kalendarId)
        {
            Console.WriteLine($"[Email] Događaj s ID '{kalendarId}' je izbrisan.");
        }
    }
}
