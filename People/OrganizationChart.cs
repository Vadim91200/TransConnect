using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    /// <summary>
    /// The OrganizationChart class represents the structure of an organization. It contains a CEO, and methods to display the organization chart, fire an employee, find an employee by their social security number and get all the employees.
    /// </summary>
    public class OrganizationChart
    {
        /// <summary>
        /// The CEO of the organization
        /// </summary>
        public Salarie Ceo { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationChart"/> class with a CEO.
        /// </summary>
        /// <param name="ceo">The CEO of the organization</param>
        public OrganizationChart(Salarie ceo)
        {
            this.Ceo = ceo;
        }
        /// <summary>
        /// Display the organization chart of the given employee, indented by the level
        /// </summary>
        /// <param name="employee">Employee to display the organization chart from</param>
        /// <param name="level">Indentation level</param>
        public void DisplayOrganizationChart(Salarie employee, int level = 0)
        {
            // Print the employee's name and title, indented by the level
            Console.WriteLine($"{new string(' ', level * 2)}{employee.Name} ({employee.Title})");

            // Recursively print the direct reports of this employee
            foreach (var directReport in employee.Directreports)
            {
                DisplayOrganizationChart(directReport, level + 1);
            }
        }
        /// <summary>
        /// Fires the employee with the given social security number
        /// </summary>
        /// <param name="nss">Social security number of the employee to be fired</param>
        public void FireEmployee(long nss)
        {
            Salarie employeeToFire = FindEmployeeBySocialSecurityNumber(nss, this.Ceo);
            if (employeeToFire != null)
            {
                // Find the employee's manager
                if (employeeToFire.Manager != null)
                {
                    // Remove the employee from the manager's direct reports
                    employeeToFire.Manager.Directreports.Remove(employeeToFire);
                }
            }
        }
        /// <summary>
        /// Finds the employee with the given social security number
        /// </summary>
        /// <param name="socialSecurityNumber">Social security number of the employee to find</param>
        /// <param name="employee">Employee to start the search from</param>
        /// <returns>The employee with the given social security number, or null if not found</returns>
        public Salarie FindEmployeeBySocialSecurityNumber(long socialSecurityNumber, Salarie employee)
        {
            if (employee.Nss == socialSecurityNumber)
            {
                return employee;
            }

            foreach (Salarie directReport in employee.Directreports)
            {
                Salarie matchingEmployee = FindEmployeeBySocialSecurityNumber(socialSecurityNumber, directReport);
                if (matchingEmployee != null)
                {
                    return matchingEmployee;
                }
            }

            return null;
        }
        public List<Salarie> GetAllEmployees(Salarie employee)
        {
            // Create a list to hold the employees
            List<Salarie> allEmployees = new List<Salarie>();

            // Add the current employee to the list
            allEmployees.Add(employee);

            // Recursively add the direct reports' subemployees to the list
            foreach (Salarie directReport in employee.Directreports)
            {
                allEmployees.AddRange(GetAllEmployees(directReport));
            }

            return allEmployees;
        }
    }
}
