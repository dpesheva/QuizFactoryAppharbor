﻿namespace QuizFactory.Data.Common
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using QuizFactory.Data.Common.Interfaces;

    public abstract class AuditInfo : IAuditInfo
    {
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Specifies whether or not the CreatedOn property should be automatically set.
        /// </summary>
        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
    }
}