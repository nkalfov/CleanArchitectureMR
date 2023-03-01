using System;
using CleanArchitecture.Common.Contracts;

namespace CleanArchitecture.Common
{
    public class DateService : IDateService
    {
        public DateTime GetDate()
        {
            return DateTime.Now.Date;
        }
    }
}

