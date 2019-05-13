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
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabel label;
        public LabelController(ILabel label)
        {
            this.label = label;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="labelmodel">The labelmodel.</param>
        /// <returns></returns>
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
        /// Deletes the Label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return result</returns>
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
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
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
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }
        
    }
}