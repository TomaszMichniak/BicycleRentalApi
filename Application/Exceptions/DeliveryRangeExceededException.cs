using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class DeliveryRangeExceededException : Exception
    {
        public DeliveryRangeExceededException(string message) : base(message)
        {
        }
    }
}
