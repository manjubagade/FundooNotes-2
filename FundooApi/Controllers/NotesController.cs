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
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
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
                Console.WriteLine(e.Message);
                return this.BadRequest();

            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return result</returns>
        [HttpDelete]
        [Route("deleteNotes/{id}")]
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


        //[HttpPost]
        //[Route("image/{Id}")]
        //public IActionResult AddImage(IFormFile file,int Id)
        //{
        //   // if (file == null)
        //   // {
        //   //     return this.BadRequest();
        //   // }
        //   //else
        //   // {
        //        return null;
        //   //   //  var result = this.notesHandler.AddImage(file, id);
        //   //    // return this.Ok(new { result });
        //   // }
        //}

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
        /// Views all.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return notes</returns>
        [HttpGet]
        [Route("viewNotes/{UserId}")]
        public IActionResult ViewAll(Guid userId)
        {
            try
            {
                IList<Notes> note = this.notesHandler.AccessNotes(userId);
                return this.Ok(note);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("image/{Id}")]
        public IActionResult Image(IFormFile file, int id)
        {
            if (file == null)
            {
                return this.NotFound("The file couldn't be found");
            }

            // var result = this.notesBusiness.Image(file, id);
            // return this.Ok(new { result });
            return null;
        }
    }
}