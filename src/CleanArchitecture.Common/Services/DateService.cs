using System;
using CleanArchitecture.Common.Services.Contracts;

namespace CleanArchitecture.Common.Services
{
    public class DateService : IDateService
    {
        public DateTime GetDate()
        {
            return DateTime.Now.Date;
        }
    }
}

