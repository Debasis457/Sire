using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Entities.Question
{
    public class QuestionResponse : BaseEntity
    {
        public ResponseTypes Response_Type { get; set; }
        public Response_Type_Category Response_Type_Cateogary { get; set; }
        public string Value { get; set; }
        public int? QuestionId { get; set; }

    }


    public enum Response_Type_Category
    {
        Binary,
        None,
        Graduated,
        NotAnswerable
    }


    public enum ResponseTypes
    {
        Hardware,
        Process,
        Human
    }
}
