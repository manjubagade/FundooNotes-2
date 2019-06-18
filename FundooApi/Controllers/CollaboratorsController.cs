// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorsController.cs" company="Bridgelabz">
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
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CollaboratorsController class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
            [ApiController]
            [Authorize]
            public class CollaboratorsController : ControllerBase
            {
        /// <summary>
        /// The collaborators
        /// </summary>
        private readonly ICollborators collborators;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorsController"/> class.
        /// </summary>
        /// <param name="collborators">The ICollborators.</param>
        public CollaboratorsController(ICollborators collborators)
        {
            this.collborators = collborators;
        }
        
        /// <summary>
        /// Creates the Collaborator.
        /// </summary>
        /// <param name="collaboratorsModel">The Collaborators.</param>
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
        /// Deletes the Collaborator.
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
        /// Updates the Collaborators.
        /// </summary>
        /// <param name="collaboratorsModel">The Collaborators.</param>
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

        /// <summary>
        /// Views all.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of Result</returns>
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
