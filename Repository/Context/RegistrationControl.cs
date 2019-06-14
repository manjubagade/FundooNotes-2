// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistrationControl.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
namespace FundooApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// class RegistrationControl
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext" />
    public class RegistrationControl : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationControl"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.</param>
        public RegistrationControl(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>
        /// The application.
        /// </value>
        public DbSet<ApplicationUser> Application { get; set; }

        /// <summary>
        /// Gets or sets the get notes.
        /// </summary>
        /// <value>
        /// The get notes.
        /// </value>
        public DbSet<Notes> Notes { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public DbSet<Label> Labels { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public DbSet<Image> Images { get; set; }

        /// <summary>
        /// Gets or sets the notes labels.
        /// </summary>
        /// <value>
        /// The notes labels.
        /// </value>
        public DbSet<NotesLabel> NotesLabels { get; set; }

        /// <summary>
        /// Gets or sets the collaborators.
        /// </summary>
        /// <value>
        /// The collaborators.
        /// </value>
        public DbSet<Collaborators> Collaborators { get; set; }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        /// been sent successfully to the database.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous save operation. The task result contains the
        /// number of state entries written to the database.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </para>
        /// <para>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </para>
        /// </remarks>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
       {
           var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
            ////Add Entity with Current Datetime to CreatedDate column
           addedEntities.ForEach(E =>
           {
               E.Property("CreatedDate").CurrentValue = DateTime.Now;
           });

           var editedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
            ////Add Entity with Current Datetime to ModifiedDate column
           editedEntities.ForEach(e =>
           {
               e.Property("ModifiedDate").CurrentValue = DateTime.Now;
           });
            
           return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
       }
    }
}