using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication1.Models.Interfaces
{
    /// <summary>
    /// Interface of the specific person
    /// </summary>
    public interface IPerson
    {
        /// <summary>
        /// Represents ID of this person. [0, infinity]
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Represents first name of this person.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Represents first name of this person.
        /// </summary>
        /// <remarks>
        /// The default value is null which means it hasn't been defined yet.
        /// </remarks>
        string MiddleName { get; set; }

        /// <summary>
        /// Represents first name of this person.
        /// </summary>
        string LastName { get; set; }
    }
}
