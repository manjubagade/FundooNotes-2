namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class NotesLabel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [ForeignKey("Notes")]
        public int NotesId { get; set; }

        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        /// <value>
        /// The label identifier.
        /// </value>
        [ForeignKey("Label")]
        public int LabelId { get; set; }

        /// <summary>
        /// Gets or sets the collaborators identifier.
        /// </summary>
        /// <value>
        /// The collaborators identifier.
        /// </value>
        [ForeignKey("Collaboratrors")]
        public int CollaboratorsId { get; set; }
    }
}