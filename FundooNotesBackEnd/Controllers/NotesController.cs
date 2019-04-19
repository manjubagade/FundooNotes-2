using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using FundooNotesBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly UserManager<Notes> userManager;

        NotesController(UserManager<Notes> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddNotes(Notes notes)
        {
            try
            {
                ////Assign Variables
                var Notes = new Notes()
                {
                    Title = notes.Title,
                    Description = notes.Description
                };
               var result= await this.userManager.CreateAsync(Notes);
                if(result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest("Notes Not Added");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}