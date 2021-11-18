using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Common.Enums
{
    public enum StatusEnum
    {
        // enumeration definition goes here
        Active = 1,
        Deleted = 2,
        Error = 3,
        Success = 4,
        Inactive = 5,
        Waiting = 6,
        Accepted = 7
    }
    public enum ReservationStatusEnum
    {
        // enumeration definition goes here
        Reservated = 1,
        Canceled = 2
    }
    public enum GenderEnum {
        Female = 0,
        Male = 1 
    }
}
