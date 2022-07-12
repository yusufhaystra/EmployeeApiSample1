using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeApplication1.Models.Interfaces
{
    public interface ICheckingResult
    {
        /// <summary>
        /// Represents the state of this process has been confirmed or not. True: It has been confirmed. False: It has not been confirmed and described in ErrorMessage
        /// </summary>
        bool Confirmed { get; set; }

        /// <summary>
        /// Represents the reason of why this process cannot be confirmed if Confirmed is false.
        /// </summary>
        string Error { get; set; }
    }
}
