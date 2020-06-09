using AMR_Server.Application.Common.Interfaces;
using System;

namespace AMR_Server.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
