using Sire.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sire.Data.Entities.Training
{
    public class Training_Task :BaseEntity
    {
        public string Wbs_Number { get; set; }
        public string Task_Title { get; set; }
        public string Assessor { get; set; }
        public string Reviewer { get; set; }
        public int? Master { get; set; }
        public int? CH_Off { get; set; }
        public int? Second_Off { get; set; }
        public int? Third_Off { get; set; }
        public int? Jr_Off { get; set; }
        public int? CH_Eng { get; set; }
        public int? Second_Eng { get; set; }
        public int? Third_Eng { get; set; }
        public int? Fourth_Eng { get; set; }
        public int? ETO_Sr { get; set; }
        public int? ETO_Jr { get; set; }
        public int? Cargo_Eng_Sr { get; set; }
        public int? Cargo_Eng_Jr { get; set; }
        public int? Deck_Rating { get; set; }
        public int? Eng_Rating { get; set; }
        public int? Catering { get; set; }
        public string Others { get; set; }
        public string Accompanying_Officer { get; set; }
        public string Familiar1_RG_1 { get; set; }
        public string Familiar1_RG_2 { get; set; }
        public string Familiar1_Procedure { get; set; }
        public string Familiar2_RG_1 { get; set; }
        public string Familiar2_RG_2 { get; set; }
        public string Familiar2_Procedure { get; set; }
        public string Interview1_RG1 { get; set; }
        public string Interview1_RG2 { get; set; }
        public string Interview1_Procedure { get; set; }
        public string Interview2_RG1 { get; set; }
        public string Interview2_RG2 { get; set; }
        public string Interview2_Procedure { get; set; }
        public string Demonstrate1_RG1 { get; set; }
        public string Demonstrate1_RG2 { get; set; }
        public string Demonstrate1_Procedure { get; set; }
        public string Demonstrate2_RG1 { get; set; }
        public string Demonstrate2_RG2 { get; set; }
        public string Demonstrate2_Procedure { get; set; }
        public string Manuals_Plans_Procedures { get; set; }
        public string Certificates_Checklists_Records { get; set; }
        public string LogBooks_Entries { get; set; }

    }
}
