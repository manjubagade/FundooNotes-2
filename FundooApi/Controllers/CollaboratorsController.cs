using BusinessLayer.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Threading.Tasks;

         namespace FundooApi.Controllers
        {
            [Route("api/[controller]")]
            [ApiController]
            [Authorize]
            public class CollaboratorsController: ControllerBase
            {

         private readonly ICollborators collborators;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesHandler">The notes creation.</param>
        public CollaboratorsController(ICollborators collborators)
        {
            this.collborators = collborators;
        }


        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return result</returns>
        [HttpPost]
        [Route("addCollaborators")]
        public async Task<IActionResult> CreateCollaborators([FromBody]Collaborators collaboratorsModel)
        {
            try
            {
                var result = await this.collborators.Create(collaboratorsModel);
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
        public async Task<IActionResult> DeleteCollaborators(int id)
        {
            try
            {
                var result = await this.collborators.Delete(id);
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
        [Route("updatecollaborators/{id}")]
        public async Task<IActionResult> UpdateNotes(Collaborators collaboratorsModel, int id)
        {
            try
            {
                var result = await this.collborators.Change(collaboratorsModel, id);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("viewcollaborators/{UserId}")]
        public IActionResult ViewAll(string userId)
        {
            try
            {
                IList<CollaboratorMap> note = this.collborators.AccessCollaborators(userId);
                return this.Ok(note);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }
    }
}
