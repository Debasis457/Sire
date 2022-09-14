
using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sire.Data.Entities.Training
{
    public class Training_Question : BaseEntity
    {

        [ForeignKey("Training")]
      
      
        public int Training_Id { get; set; }

        [ForeignKey("User")]
       
        public int Trainee_Id { get; set; }
        [ForeignKey("Question")]
        public int Question_Id { get; set; }

        public bool? Completed { get; set; }

    }
}
