using System;
using TinyURL.Services.Interfaces;

namespace TinyURL.Services
{
    public class DateService : IDateService
    {
        public DateTime CurrentDate()
        {
            return DateTime.Now;
        }
    }
}
