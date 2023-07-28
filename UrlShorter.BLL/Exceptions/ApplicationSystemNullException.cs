using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShorter.BLL.Exceptions
{
    public class ApplicationSystemNullException<T> : ApplicationSystemBaseException
    {
        public ApplicationSystemNullException(string argument)
            : base($"При работе класса {typeof(T)} отсутствует необходимый параметр {argument}.")
        {
        }
    }
}
