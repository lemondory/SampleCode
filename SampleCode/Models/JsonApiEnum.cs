using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models
{
    public enum ErrorCode
    {
        ErrorNone = 0,
        SystemError = 1,
        DatabaseError = 2,
        ParameterError = 4,
    }
}
