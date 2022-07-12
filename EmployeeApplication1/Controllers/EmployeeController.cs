using EmployeeApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeApplication1.Controllers
{
    /// <summary>
    /// Represents the controller where all the employee' are shared
    /// </summary>
    public class EmployeeController : ApiController
    {
        /// <summary>
        /// Represents all the employees
        /// </summary>
        private static List<Employee> Employees;

        /// <summary>
        /// Constructor to intiliaze the employee controller
        /// </summary>
        public EmployeeController()
        {
            if (Employees == null)
            {
                Employees = new List<Employee>()
                { // Sample employees
                    new Employee(1, "Yusuf", "Sahin", "Cihan"),
                    new Employee(){ ID = 2, FirstName = "John", LastName = "Smith"},
                    new Employee(3, "Elly", "Jackson", "Lewis"),
                    new Employee(4, "Daily", "Sahin"),
                };
            }
        }


        /// <summary>
        /// Gets the employee that you defined ID with <paramref name="EmployeeID"/> parameter if the employee exists. If not, gets the default value.
        /// </summary>
        /// <param name="EmployeeID">Represents ID of the employee that you want to get. [0, infinity]</param>
        /// <returns>Returns the employee that you defined ID with <paramref name="EmployeeID"/> parameter if the employee exists. If not, returns the default value.</returns>
        [Route("api/Employee/GetEmployeeByID/{EmployeeID:int}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeByID([FromUri]int EmployeeID)
        {
            var check = Employee.CheckIDRule(EmployeeID);
            if (check.Confirmed)  // If EmployeeID is in the correct range,
            {
                //     It returns the first matched employee that ID is the same with EmployeeID if the employee exists.
                //     If not, it returns the defaul value of Employee class which is null.
                return Request.CreateResponse(Employees?.FirstOrDefault(ax => ax.ID == EmployeeID));
            }
            else // If EmployeeID is not in the correct range,
            {
                //  throws an exception. No need to search all the employess and use CPU just because of the meaningless EmployeeID because it will never be matched.
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, check.Error); // There is a problem with the parameters given.
            }
        }

        /// <summary>
        /// Gets the employees that matches their FirstName with <paramref name="FirstName"/> that you defined.
        /// </summary>
        /// <returns>Returns all employees that matches their FirstName with <paramref name="FirstName"/> that you defined.</returns>
        [Route("api/Employee/GetEmployees/{FirstName:string}/{LastName:string}")]
        [HttpGet]
        public HttpResponseMessage GetEmployees(string FirstName = null, string LastName = null)
        {
            if (FirstName is null && LastName is null) return Request.CreateResponse(Employees); // The client wants to get all employees without querying.

            // If the client wants to query on the list:

            if (LastName is null) // Means the client wants to get employees by their first names.
            {
                
                var check = Employee.CheckNameRule(FirstName);
                if (check.Confirmed)            // If FirstName is not in the correct range,
                {
                    // throws an exception. No need to search all the employess and use CPU just because of the meaningless FirstName because it will never be matched.
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, check.Error); // There is a problem with the parameters given.

                }
                FirstName = FirstName.ToLower();
                // Searching the employees:

                return Request.CreateResponse(Employees?.Where(ax => ax.FirstName.ToLower() == FirstName));
            }
            else if (FirstName is null)
            {
                // Means the client wants to get employees by their last names.

                var check = Employee.CheckNameRule(LastName);
                if (!check.Confirmed)              // If LastName is not in the correct range, 
                {
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, check.Error);
                }

                LastName = LastName.ToLower();

                return Request.CreateResponse(Employees?.Where(ax => ax.LastName.ToLower() == LastName));
            } 
            else
            {
                // Means the client wants to get employees by their first and last name
                var check = Employee.CheckNameRule(FirstName);
                var check2 = Employee.CheckNameRule(LastName);
                if (!check.Confirmed) return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, check.Error);
                if (!check2.Confirmed) return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, check2.Error);

                FirstName = FirstName.ToLower();
                LastName = LastName.ToLower();

                return Request.CreateResponse(Employees?.Where(ax => ax.FirstName.ToLower() == FirstName && ax.LastName.ToLower() == LastName));
            }

        }

        /// <summary>
        /// Posts <paramref name="NewEmployee"/> to the employees list. 
        /// </summary>
        /// <param name="NewEmployee">The employee that will be added</param>
        /// <returns>Returns the response message that represents the state of posting process is completed sucessfully or not</returns>
        [Route("api/Employee/Post/{NewEmployee:Employee}")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Employee NewEmployee)
        {
            // Checking the new employee value is in the right format:
            {
                var check = Employee.IsNullOrEmpty(NewEmployee);
                if (!check.Confirmed) return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, check.Error); 
            } // Calling destructor of 'check' value to use less memory.

            // Checking there is already an employee who has the same ID. ID must be unique.
            if (Employees.Any(ax => ax.ID == NewEmployee.ID))
            {
                // Means there is already an employee who has the same ID.
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, $"There is already an employee with the same ID ({NewEmployee.ID}). Please check ID of the employee that you want to post (add) is unique.");
            }

            // There is nothing to worry about to adding this employee to the list.
            Employees?.Add(NewEmployee);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/Employee/Delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(int EmployeeID)
        {
            // Checking EmployeeID is in the right format:
            {
                var check = Employee.CheckIDRule(EmployeeID);
                if (!check.Confirmed) return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, check.Error);
            }  // Calling destructor of 'check' value to use less memory.

            int employeeIndex = Employees.FindIndex(ax => ax.ID == EmployeeID); // Represents index of the current employee who will be deleted from the list. If there is no employee, it will be equal to -1.

            // If the employee could not be found in the list.
            if (employeeIndex == -1) return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, $"There is no employee who IDs equals to {EmployeeID}. Please check the employee that you want to delete from the list.");

            // If the employee is found, I will delete it.
            Employees.RemoveAt(employeeIndex);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
