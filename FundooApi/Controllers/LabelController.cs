// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
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
    using Common.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// LabelController controller class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// The label
        /// </summary>
        private readonly ILabel label;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        public LabelController(ILabel label)
        {
            this.label = label;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="labelmodel">The Label.</param>
        /// <returns>return IActionResult</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateLabel(Label labelmodel)
        {
            try
            {
                var result = await this.label.Create(labelmodel);
                return this.Ok(result);
            }
            catch (Exception e)
            {
              throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Adds the notes label.
        /// </summary>
        /// <param name="labelmodel">The NotesLabel.</param>
        /// <returns>result data</returns>
        /// <exception cref="Exception">The Exception</exception>
        [HttpPost]
        [Route("addlabel")]
        public async Task<IActionResult> AddNotesLabel(NotesLabel labelmodel)
        {
            try
            {
                var result = await this.label.AddLabel(labelmodel);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// DeletenotesLabel the label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        [HttpDelete]
        [Route("deletenoteslabel/{id}")]
        public async Task<IActionResult> DeletenotesLabel(int id)
        {
            try
            {
                var result = await this.label.DeleteNotesLabel(id);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Views all notes label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of notes</returns>
        [HttpGet]
        [Route("viewalllabel/{userId}")]
        public IActionResult ViewAllNotesLabel(string userId)
        {
            IList<NotesLabel> result = this.label.ViewNotesLabel(userId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// Deletes the Label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return result in IActionResult</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteLabel(int id)
        {
            try
            {
                var result = await this.label.Delete(id);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The Label.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return result</returns>
        [HttpPut]
        [Route("updateLabel/{id}")]
        public async Task<IActionResult> UpdateLabel(Label labelModel, int id)
        {
            try
            {
                var result = await this.label.Change(labelModel, id);
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
        /// <returns>return Label</returns>
        [HttpGet]
        [Route("viewLabel/{UserId}")]
        public IActionResult ViewAll(Guid userId)
        {
            try
            {
                IList<Label> label = this.label.AccessLabel(userId);
                return this.Ok(label);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("viewlabelnotes/{userId}")]
        public IActionResult ViewLabelNotes(NotesLabel notesLabelmodel)
        {
            IList<Notes> result = this.label.ViewLabelNotes(notesLabelmodel);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

    }
}