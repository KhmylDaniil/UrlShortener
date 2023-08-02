using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Abstractions;

namespace UrlShortener.BLL.Services
{
    /// <summary>
    /// Реализация провайдера времени 
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// Текущее время по UTC
        /// </summary>
        public DateTime Now => DateTime.UtcNow;
    }
}
