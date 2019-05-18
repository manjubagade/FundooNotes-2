using BusinessLayer.Services;
using FundooApi;
using FundooApi.Controllers;
using Moq;
using RepositoryLayer.Interface;
using System;
using Xunit;

namespace Testing
{
    public class UnitTest1
    {
        [Fact]
        public void Create()
        {
            ////arrange
            var service = new Mock<IRepositoryNotes>();
            var notes = new NotesCreation(service.Object);
            var Note = new Notes()
            {
                Id = 5,
                Title = "Abc",
                Description = "Pqr mno",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            ////act
            var data = notes.Create(Note);

            ////assert
            Assert.NotNull(data);
        }

        [Fact]
        public void Delete()
        {
            ////arrange
            var service = new Mock<IRepositoryNotes>();
            var notes = new NotesCreation(service.Object);

            ////act
            var data = notes.Delete(2);

            ////assert
            Assert.NotNull(data);
        }

        [Fact]
        public void Update()
        {
            ////arrange
            var service = new Mock<IRepositoryNotes>();
            var notes = new NotesCreation(service.Object);
            //Notes note = notes.
            //note.Title = model.Title;
            //notes.Description = model.Description;
            //notes.Image = model.Image;
            //notes.Color = model.Color;
            ////act
          //  var data = notes.Change(1);

            ////assert
           // Assert.NotNull(data);
        }
    }
}
