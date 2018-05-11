using System;
using System.Collections.Generic;
using Xunit;
using WebApp.Models;
using WebApp.Data.Repository.Interfaces;
using WebApp.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAppTest.Mocks;

namespace WebAppTest
{
    public class ArtistControllerTest
    {
        [Fact]
        public async Task Controller_ShouldReturnIndex()
        {
            //Arrange
            var items = new List<Artist>
                {
                    new Artist(),
                    new Artist(),
                    new Artist()
                };

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetAll()).ReturnsAsync(items);
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Index("") as ViewResult;

            //Assert
            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public async Task Index_SearchNameShouldReturnOnlyOneArtist()
        {
            //Arrange
            var items = new List<Artist>
                {
                    new Artist("Trivium", 4, "Metal", "Kalifornia"),
                    new Artist("Metallica", 4, "Metal", "Detroit"),
                    new Artist("Slawomir", 1, "RockoPolo", "Sosnowiec")
                };

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetAll()).ReturnsAsync(items);
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Index("Trivium") as ViewResult;

            //Assert
            var model = (IList<Artist>)result.Model;
            Assert.Equal(1, model.Count);
        }

        [Fact]
        public async Task Index_SearchGenreShouldReturnProperNumberOfArtist()
        {
            //Arrange
            var items = new List<Artist>
                {
                    new Artist("Trivium", 4, "Metal", "Kalifornia"),
                    new Artist("Metallica", 4, "Metal", "Detroit"),
                    new Artist("Slawomir", 1, "RockoPolo", "Sosnowiec")
                };

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetAll()).ReturnsAsync(items);
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Index("Metal") as ViewResult;

            //Assert
            var model = (IList<Artist>)result.Model;
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Controller_ShouldAddProperNumberOfArtists()
        {
            //Arrange
            var items = new List<Artist>
            {
                new Artist(),
                new Artist(),
                new Artist()
            };

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetAll()).ReturnsAsync(items);
            var controller = new ArtistsController(mockRepository.Object);

            // Act
            var result = await controller.Index("") as ViewResult;

            // Assert
            var model = (IList<Artist>)result.Model;
            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task Index_SearchWrongStringShlouldReturnEmptyList()
        {
            //Arrange
            var items = new List<Artist>
                {
                    new Artist("Trivium", 4, "Metal", "Kalifornia"),
                    new Artist("Metallica", 4, "Metal", "Detroit"),
                    new Artist("Slawomir", 1, "RockoPolo", "Sosnowiec")
                };

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetAll()).ReturnsAsync(items);
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Index("Reagge") as ViewResult;

            //Assert
            var model = (IList<Artist>)result.Model;
            Assert.Equal(0, model.Count);
        }

        [Fact]
        public async Task Index_NoModelsShouldReturnEmptyModel()
        {
            //Arrange
            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Artist>());
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Index("Metal");

            //Assert
            var viewResult = result as ViewResult;
            var model = (IList<Artist>)viewResult.Model;
            Assert.Empty(model);
        }

        [Fact]
        public async Task Controller_ShouldReturnDetailsView()
        {
            //Arrange
            var artist = new Artist("Trivium", 4, "Metal", "Kalifornia");

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetById("1")).ReturnsAsync(artist);
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Details(1) as ViewResult;

            //Assert
            Assert.Equal("Details", result.ViewName);
        }

        [Fact]
        public async Task Details_ExistingIdShouldGiveCorrectModel()
        {
            //Arrange
            var artist1 = new Artist("Trivium", 4, "Metal", "Kalifornia");
            var artist2 = new Artist("Metallica", 4, "Metal", "Detroit");

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetById("1")).ReturnsAsync(artist1);
            mockRepository.Setup(x => x.GetById("2")).ReturnsAsync(artist2);
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Details(1) as ViewResult;

            //Assert
            var artist = result.Model as Artist;
            Assert.Equal("Trivium", artist.Name);
        }

        [Fact]
        public async Task Details_NotExistingArtistIdShouldReturnNotFound()
        {
            //Arrange
            var artist1 = new Artist("Trivium", 4, "Metal", "Kalifornia");
            var artist2 = new Artist("Metallica", 4, "Metal", "Detroit");

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetById("1")).ReturnsAsync(artist1);
            mockRepository.Setup(x => x.GetById("2")).ReturnsAsync(artist2);
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Details(4) as ViewResult;

            //Assert
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public async Task Details_NullArtistShouldReturnNotFound()
        {
            //Arrange
            var mockRepository = new Mock<IArtistRepository>();
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Details(null) as ViewResult;

            //Assert
            Assert.Equal("NotFound", result.ViewName);
        }

        [Fact]
        public void Controller_ShouldReturnCreateView()
        {
            //Arrange
            var mockRepository = new Mock<IArtistRepository>();
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = controller.Create() as ViewResult;

            //Assert
            Assert.Equal("Create", result.ViewName);
        }

        [Fact]
        public async Task Create_CreateArtistShouldAddCorrectly()
        {
            //Arrange
            var artist1 = new Artist("Trivium", 4, "Metal", "Kalifornia");

            var mockRepository = new ArtistRepositoryMock();
            var controller = new ArtistsController(mockRepository);

            //Act
            await controller.Create(artist1);

            //Assert
            var artist = await mockRepository.GetById("1");
            Assert.Equal("Trivium", artist.Name);
        }

        [Fact]
        public async Task Create_AfterCreateShouldRedirectToIndex()
        {
            //Arrange
            var artist1 = new Artist("Trivium", 4, "Metal", "Kalifornia");

            var mockRepository = new Mock<IArtistRepository>();
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Create(artist1) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Controller_ShouldReturnEditView()
        {
            //Arrange
            var artist1 = new Artist("Trivium", 4, "Metal", "Kalifornia");
            var artist2 = new Artist("Metallica", 4, "Metal", "Detroit");

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetById("1")).ReturnsAsync(artist1);
            mockRepository.Setup(x => x.GetById("2")).ReturnsAsync(artist2);
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Edit(1) as ViewResult;

            //Assert
            Assert.Equal("Edit", result.ViewName);
        }

        [Fact]
        public async Task Edit_CorrectUpdateShouldEditArtist()
        {
            //Arrange
            var artist1 = new Artist("Trivium", 4, "Metal", "Kalifornia");
            var artist2 = new Artist("Metallica", 4, "Metal", "Detroit");

            var mockRepository = new ArtistRepositoryMock();
            var controller = new ArtistsController(mockRepository);
            await controller.Create(artist1);
            await controller.Create(artist2);
            //Act
            artist2.Name = "Megadeth";
            await controller.Edit(1);

            //Assert
            var artist = await mockRepository.GetById("2");
            Assert.Equal("Megadeth", artist.Name);
        }

        [Fact]
        public async Task Controller_ShouldReturnDeleteView()
        {
            //Arrange
            var artist1 = new Artist("Trivium", 4, "Metal", "Kalifornia");
            var artist2 = new Artist("Metallica", 4, "Metal", "Detroit");

            var mockRepository = new Mock<IArtistRepository>();
            mockRepository.Setup(x => x.GetById("1")).ReturnsAsync(artist1);
            mockRepository.Setup(x => x.GetById("2")).ReturnsAsync(artist2);
            var controller = new ArtistsController(mockRepository.Object);

            //Act
            var result = await controller.Delete(1) as ViewResult;

            //Assert
            Assert.Equal("Delete", result.ViewName);
        }

        [Fact]
        public async Task Delete_RemoveArtistShouldDecreaseListCount()
        {
            //Arrange
            var artist1 = new Artist("Trivium", 4, "Metal", "Kalifornia");
            var artist2 = new Artist("Metallica", 4, "Metal", "Detroit");

            var mockRepository = new ArtistRepositoryMock();
            var controller = new ArtistsController(mockRepository);
            await controller.Create(artist1);
            await controller.Create(artist2);
            //Act
            await controller.DeleteConfirmed(1);

            //Assert
            var artists = await mockRepository.GetAll() as List<Artist>;
            Assert.Equal(1, artists.Count);
        }

        [Fact]
        public async Task Edit_GiveDiffrentIdReturnNotFoundView()
        {
            var artist1 = new Artist("Trivium", 4, "Metal", "Kalifornia");

            var repositoryMock = new Mock<IArtistRepository>();

            var controller = new ArtistsController(repositoryMock.Object);
            var result = await controller.Edit(6) as ViewResult;
            Assert.Equal("NotFound", result.ViewName);
         }
    }
}
