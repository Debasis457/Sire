using System.Collections.Generic;
using Sire.Data.Entities.Common;
using Sire.Helper;

namespace Sire.Data.Dto.Master
{
    public class DropDownDto : BaseDto
    {
        public string Value { get; set; }
        public string Code { get; set; }
        public object ExtraData { get; set; }
    }
   
    public class DropDownWithSeqDto : BaseDto
    {
        public string Value { get; set; }
        public string Code { get; set; }
        public int SeqNo { get; set; }
    }

    public class DropDownEnum : BaseDto
    {
        public short Id { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }
 
}