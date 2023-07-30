using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.BLL.Exceptions
{
    public class NotValidLongUrlException : Exception
    {
        public NotValidLongUrlException() : base("Представленный url не действителен.") { }
    }
}
