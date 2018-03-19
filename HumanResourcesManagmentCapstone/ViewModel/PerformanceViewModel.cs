/*
* Description: .
* Author: Zee
* Due date: 20/03/2018
*/
using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    public class PerformanceViewModel
    {
        public PerformanceViewModel()
        {
            Constituents = new List<Constituent>();
        }

        public int PerformanceId { get; set; }

        public decimal KPI { get; set; }

        public decimal Discipline { get; set; }

        public string Status { get; set; }

        public DateTime IssueDate { get; set; }

        public string Comment { get; set; }

        public DecisionType? Decision { get; set; }

        public DateTime? CreationDate { get; set; }

        public int EmployeeId { get; set; }

        public List<Constituent> Constituents { get; set; }

        public string Employee { get; set; }
    }
}