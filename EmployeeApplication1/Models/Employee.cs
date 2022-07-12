using System;
using EmployeeApplication1.Models.Interfaces ;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;

namespace EmployeeApplication1.Models
{
    /// <summary>
    /// Represents the specific employee
    /// </summary>
    public class Employee : IPerson
    {
        #region Values

        private int _ID;
        private string _FirstName;
        private string _LastName;

        #endregion

        #region Properties

        public int ID 
        { 
            get => this._ID;
            set 
            {
                var check = CheckIDRule(value);
                if (check.Confirmed) this._ID = value;
                else throw new HttpRequestException(check.Error);
            }
        }

        public string FirstName
        {
            get => this._FirstName;
            set
            {
                var check = CheckNameRule(value);
                if (check.Confirmed) this._FirstName = value;
                else throw new HttpRequestException(check.Error);
            }
        }

        public string MiddleName { get; set; }

        public string LastName
        {
            get => this._LastName;
            set
            {
                var check = CheckNameRule(value);
                if (check.Confirmed) this._LastName = value;
                else throw new HttpRequestException(check.Error);
            }
        }

        #endregion

        /// <summary>
        /// Constructor to create an employee with <paramref name="id"/>, <paramref name="firstName"/>, <paramref name="lastName"/> and <paramref name="middleName"/>
        /// </summary>
        /// <param name="id">ID of this employee [0, infinity] or {-1} to define its undefined</param>
        /// <param name="firstName">First name of this employee</param>
        /// <param name="lastName">Last name of this employee</param>
        /// <param name="middleName">Middle name of this employee (if it does exist.)</param>
        public Employee(int id, string firstName, string lastName, string middleName = null)
        {
            // The process of the controlling the parameters has already been defined inside of the each necessary properties. I don't need to check them anymore.
            ID = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        /// <summary>
        /// Constructor to create an employee without passing any parameter and setting them later.
        /// </summary>
        public Employee()
        {
            MiddleName = null; // Default value of the Middle name 
        }

        public override string ToString()
        {
            // I write this function to view the values quicker while debugging and there would be meaningful text just in case
            return $"[{ID}] {FirstName} {LastName}"; 
        }


        #region Static Methods

        // In normally, I would create another class to check the specific rules staticly on another folder. But just for this case there is only an Employee class, that's why I thought it would be enough to check them in below.

        /// <summary>
        /// Checks <paramref name="Name"/> (such as first name or last name) is in the correct form according to the current rules. 
        /// </summary>
        /// <param name="Name">Name that will be checked (such as first name or last name)</param>
        /// <returns>Returns the state of <paramref name="Name"/> is in the correct form or not.</returns>
        public static ICheckingResult CheckNameRule(string Name) 
        {
            return Name != null && Name.Length > 0 ? CheckingResult.Confirm() : CheckingResult.Deny($"{nameof(Name)} must have at least 1 letter.");
        }

        /// <summary>
        /// Checks <paramref name="ID"/> is in the correct form according to the current rules. 
        /// </summary>
        /// <param name="ID">ID that will be checked </param>
        /// <returns>Returns the state of <paramref name="ID"/> is in the correct form or not.</returns>
        public static ICheckingResult CheckIDRule(int ID)
        {
            return ID >= 0 ? CheckingResult.Confirm() : CheckingResult.Deny($"{nameof(ID)} must be equal or greater than 0. [0, infinity)"); ; 
        }

        /// <summary>
        /// Checks <paramref name="employee"/> is null or it has a value that is not in correct form.
        /// </summary>
        /// <param name="employee">Employee that will be checked</param>
        /// <returns>Returns the state of <paramref name="employee"/> is null or it has a value that is not in correct form.</returns>
        public static ICheckingResult IsNullOrEmpty(Employee employee)
        {
            // Checking the employee is null or not:
            if (employee is null) return CheckingResult.Deny("Employee cannot be null");

            // Checking first name of the employee:
            {
                var checkName = CheckNameRule(employee.FirstName);
                if (!checkName.Confirmed) return CheckingResult.Deny($"{employee.FirstName} of the employee is not in the right format."); 
            }
            // Checking last name of the employee:
            {
                var checkName = CheckNameRule(employee.LastName);
                if (!checkName.Confirmed) return CheckingResult.Deny($"{employee.LastName} of the employee is not in the right format.");
            }

            // If all the bad conditions have been checked and there is no problem:
            return CheckingResult.Confirm();
        }

        #endregion
    }
}