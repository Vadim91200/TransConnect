﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect
{
    public class OrganizationChart
    {

        private Salarie ceo;

        public OrganizationChart(Salarie ceo)
        {
            this.ceo = ceo;
        }
        public Salarie CEO { get => this.ceo;}
        public void DisplayOrganizationChart(Salarie employee, int level = 0)
        {
            // Print the employee's name and title, indented by the level
            Console.WriteLine($"{new string(' ', level * 2)}{employee.Name} ({employee.Title})");

            // Recursively print the direct reports of this employee
            foreach (var directReport in employee.DirectReports)
            {
                DisplayOrganizationChart(directReport, level + 1);
            }
        }
        public void FireEmployee(long nss)
        {
            Salarie employeeToFire = FindEmployeeBySocialSecurityNumber(nss);
            if (employeeToFire != null)
            {
                // Find the employee's manager
                if (employeeToFire.Manager != null)
                {
                    // Remove the employee from the manager's direct reports
                    employeeToFire.Manager.DirectReports.Remove(employeeToFire);
                }
            }
        }
        private Salarie FindEmployeeBySocialSecurityNumber(long socialSecurityNumber, Salarie employee = null)
        {
            if (employee.NSS == socialSecurityNumber)
            {
                return employee;
            }

            foreach (Salarie directReport in employee.DirectReports)
            {
                Salarie matchingEmployee = FindEmployeeBySocialSecurityNumber(socialSecurityNumber, directReport);
                if (matchingEmployee != null)
                {
                    return matchingEmployee;
                }
            }

            return null;
        }
    }
}
