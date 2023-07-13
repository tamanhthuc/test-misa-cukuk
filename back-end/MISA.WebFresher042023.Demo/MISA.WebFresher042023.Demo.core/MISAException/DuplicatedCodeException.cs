using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.MISAException
{
    public class DuplicatedCodeException : Exception
    {
        public DuplicatedCodeException(object userMsg) {
            Error = userMsg;
        }
        public object? Error { get; set; }
    }
}
