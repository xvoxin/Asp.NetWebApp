using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;
using Xunit;

namespace WebAppTest
{
    public class ArtistTest
    {
        [Fact]
        public void Artist_ValidModel()
        {
            var artist = new Artist
            {
                Name = "Trivium",
                MembersCount = 4,
                Genre = "Metal",
                City = "Kalifornia",
                RegistrationDate = DateTime.Now,
            };
            var context = new ValidationContext(artist, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(artist, context, result, true);
            Assert.True(valid);
        }

        [Fact]
        public void Artist_InvalidName_ShouldReturnFalse()
        {
            var artist = new Artist
            {
                Name = "x",
                MembersCount = 4,
                Genre = "Metal",
                City = "Kalifornia",
                RegistrationDate = DateTime.Now,
            };
            var context = new ValidationContext(artist, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(artist, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void Artist_NoCity_ShouldReturnFalse()
        {
            var artist = new Artist
            {
                Name = "Trivium",
                MembersCount = 4,
                Genre = "Metal",
                RegistrationDate = DateTime.Now,
            };
            var context = new ValidationContext(artist, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(artist, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void Artist_ToLongNameLength_ShouldReturnFalse()
        {
            var artist = new Artist
            {
                Name = "TriviumParadiseMetallicaMegadeth",
                MembersCount = 4,
                Genre = "Metal",
                City = "Kalifornia",
                RegistrationDate = DateTime.Now
            };
            var context = new ValidationContext(artist, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(artist, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void Artist_InvalidName_ShouldReturnProperErrorString()
        {
            var artist = new Artist
            {
                Name = "x",
                MembersCount = 4,
                Genre = "Metal",
                City = "Kalifornia",
                RegistrationDate = DateTime.Now,
            };

            var errMessage = "Name should be 3 - 20 characters length";
            var context = new ValidationContext(artist, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(artist, context, result, true);
            Assert.Equal(errMessage, result[0].ErrorMessage);
        }
    }
}
