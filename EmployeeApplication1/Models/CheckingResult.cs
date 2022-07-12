using EmployeeApplication1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EmployeeApplication1.Models
{
    public class CheckingResult : ICheckingResult
    {
        public bool Confirmed { get; set; }

        public string Error { get; set; }

        /// <summary>
        /// Default Constructor that confirms the process
        /// </summary>
        public CheckingResult()
        {
            Confirmed = true; // The process is confirmed
        }

        /// <summary>
        /// Constructor that denies the process with the <typeparamref name="TError"/>
        /// </summary>
        /// <param name="error">Error value that describes the reason of why this process cannot be confirmed.</param>
        public CheckingResult(string error)
        {
            Error = error;
            Confirmed = false; // The process is not confirmed
        }

        /// <summary>
        /// Creates an instance that the process is not confirmed because of <paramref name="error"/>
        /// </summary>
        /// <param name="error">The reason of why this process cannot be confirmed.</param>
        /// <returns>Returns the instance that the process is not confirmed because of <paramref name="error"/></returns>
        public static CheckingResult Deny(string error) { return new CheckingResult(error);  }

        /// <summary>
        /// Creates an instance that the process is confirme
        /// </summary>
        /// <returns>Returns the instance that the process is confirmed </returns>
        public static CheckingResult Confirm() { return new CheckingResult(); }
    }
}