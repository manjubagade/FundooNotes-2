using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    public class Collaborators
    {
        /// <summary>
        /// Gets or sets the collaborators identifier.
        /// </summary>
        /// <value>
        /// The collaborators identifier.
        /// </value>
        [Key]
        public int CollaboratorsId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the receiver email.
        /// </summary>
        /// <value>
        /// The receiver email.
        /// </value>
        public string ReceiverEmail { get; set; }

        [ForeignKey("Notes")]
        public string Id { get; set; }

        public string UserId { get; set; }
       
        public DateTime CreatedDate { get; set; }
     
        public DateTime ModifiedDate { get; set; }

    }
}
