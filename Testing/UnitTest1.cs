// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace Testing
{
    using System;
    using BusinessLayer.Services;
    using FundooApi;
    using FundooApi.Controllers;
    using Moq;
    using RepositoryLayer.Interface;
    using Xunit;

    /// <summary>
    /// Unit testing
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        [Fact]
        public void Create()
        {
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
         
            var data = notes.Create(Note);
           
            Assert.NotNull(data);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        [Fact]
        public void Delete()
        {
            var service = new Mock<IRepositoryNotes>();
            var notes = new NotesCreation(service.Object);
            var data = notes.Delete(2);
            
            Assert.NotNull(data);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        [Fact]
        public void Update()
        {
            var service = new Mock<IRepositoryNotes>();
            var notes = new NotesCreation(service.Object);
            var addNotes = new Notes()
            {
                Id = 0,
                Title = "Title",
                Description = "Description",
                UserId = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            
            var data = notes.Change(addNotes, 2);
           
            Assert.NotNull(data);
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        [Fact]
        public void GetNotes()
        {
            
            var service = new Mock<IRepositoryNotes>();
            var notes = new NotesCreation(service.Object);
            var data = notes.AccessNotes(Guid.NewGuid().ToString());
           
            Assert.Null(data);
        }
    }
}