﻿using FundooApi;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CreateNotes : IRepositoryNotes
    {
        private readonly RegistrationControl registrationControl;

        public CreateNotes(RegistrationControl registrationControl)
        {
            this.registrationControl = registrationControl;
        }

        public void AddNotes(Notes notes)
        {
            var addnotes = new Notes()
            {
                UserId = notes.UserId,
                Title = notes.Title,
                Description = notes.Description,
                CreatedDate = notes.CreatedDate,
                ModifiedDate = notes.ModifiedDate
            };
            var result = this.registrationControl.GetNotes.Add(addnotes);
        }

        public Task<int> SaveChangesAsync()
        {
            var result = this.registrationControl.SaveChangesAsync();
            return result;
        }
    }
}