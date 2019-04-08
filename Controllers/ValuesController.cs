// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="ValuesController.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
namespace FundooNotesBackEnd.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooNotesBackEnd.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// class ValuesController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="registrationControl">The registration control.</param>
        public ValuesController(RegistrationControl registrationControl)
        {
        }

        /// <summary>
        /// ActionResult Method
        /// </summary>
        /// <returns>the Values</returns>
        //// GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get Method
        /// </summary>
        /// <param name="id">the id.</param>
        /// <returns>the string</returns>
        //// GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Post Method
        /// </summary>
        /// <param name="value">the value.</param>
        //// POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Put Method
        /// </summary>
        /// <param name="id">the id.</param>
        /// <param name="value">the value.</param>
        //// PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Delete Method
        /// </summary>
        /// <param name="id">the id.</param>
        //// DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
