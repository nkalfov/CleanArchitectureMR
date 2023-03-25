using System;
using CleanArchitecture.Common.Services.Contracts;

namespace CleanArchitecture.Common.Services
{
    public class DateService : IDateService
    {
        public DateTimeOffset GetDate()
        {
            return DateTimeOffset.Now.Date;
        }
    }
}

