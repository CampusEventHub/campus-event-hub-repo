using CampusEventHubApi.Models;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.Tests.Models
{
    [TestFixture]
    public class KalendarTests
    {
        [Test]
        public void Kalendar_DefaultKreirano_SetToCurrentDateTime()
        {
            var kalendar = new Kalendar();
            Assert.That(kalendar.Kreirano.Date, Is.EqualTo(DateTime.Now.Date)); // Check that the date part matches today's date
        }

        [Test]
        public void Kalendar_NaslovCannotBeEmpty()
        {
            var kalendar = new Kalendar { Naslov = "" };
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(kalendar, new ValidationContext(kalendar), validationResults, true);
            Assert.That(isValid, Is.False);
            Assert.That(validationResults.Count, Is.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The Naslov field is required."));
        }

        [Test]
        public void Kalendar_VrijemeCanBeNull()
        {
            var kalendar = new Kalendar { Naslov = "Test Event", Datum = DateTime.Now };
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(kalendar, new ValidationContext(kalendar), validationResults, true);
            Assert.That(isValid, Is.True); // No validation errors, Vrijeme is optional
        }
    }
}