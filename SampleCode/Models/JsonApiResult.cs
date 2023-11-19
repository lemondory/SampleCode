using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class JsonApiResult
    {
        public bool Result { get; set; }
        public int ErrorCode { get; set; }

        public JsonApiResult()
        {
            Result = true;
            ErrorCode = 0;
        }
    }

    public class CharacterResponse : JsonApiResult
    {
        public CharacterInfo Character { get; set; }

        public CharacterResponse()
        {
            
        }
    }
}
