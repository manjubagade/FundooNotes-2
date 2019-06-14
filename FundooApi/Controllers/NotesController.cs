// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
namespace FundooApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// NotesController controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
   [Authorize]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The notes creation
        /// </summary>
        private readonly INotes notesHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesHandler">The notes creation.</param>
        public NotesController(INotes notesHandler)
        {
            this.notesHandler = notesHandler;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return result</returns>
        [HttpPost]
        [Route("addNotes")]
        public async Task<IActionResult> CreateNotes(Notes notesModel)
        {
            try
            {
                var result = await this.notesHandler.Create(notesModel);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return result</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteNotes(int id)
        {
            try
            {
                var result = await this.notesHandler.Delete(id);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return result</returns>
        [HttpPut]
        [Route("updateNotes/{id}")]
        public async Task<IActionResult> UpdateNotes(Notes notesModel, int id)
        {
            try
            {
                var result = await this.notesHandler.Change(notesModel, id);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Views all Notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return notes</returns>
        [HttpGet]
        [Route("view/{userId}")]
        public IActionResult view(string userId)
        {
            IList<Notes> result = this.notesHandler.AccessNotes(userId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// methods for uploading Image
        /// </summary>
        /// <param name="file">The IFormFile.</param>
        /// <param name="id">The id.</param>
        /// <returns>string url</returns>
        [HttpPost]
        [Route("image/{id}")]
        public async Task<IActionResult> Image(IFormFile file, int id)
        {
            if (file == null) 
            {
                return this.NotFound("The file couldn't be found");
            }

            var result = await this.notesHandler.AddImage(file, id);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Archives the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>notes data</returns>
        [HttpGet]
        [Route("archive/{userId}")]
        public IActionResult ArchiveNotes(string userId)
        {
            IList<Notes> result = this.notesHandler.Archive(userId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// Trashes the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>notes data</returns>
        [HttpGet]
        [Route("trash/{userId}")]
        public IActionResult TrashNotes(string userId)
        {
            IList<Notes> result = this.notesHandler.Trash(userId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>reminder data</returns>
        [HttpGet]
        [Route("reminder/{userId}")]
        public IActionResult Reminder(string userId)
        {
            IList<Notes> result = this.notesHandler.Reminder(userId);
            if (result == null)
            {
                return this.NotFound("no reminder");
            }

            return this.Ok(new { result });
        }

        [HttpGet]
        [Route("Alarm/{userId}")]
        public IActionResult Alarrm(string userId)
        {
            IList<Notes> result = this.notesHandler.Alarm(userId);
            if (result == null)
            {
                return this.NotFound("no reminder");
            }

            return this.Ok(new { result });
        }
    }
}