/*
* Description: View model for Network passes information between Network views and its controller.
* Author: Zee
* Due date: 03/04/2018
*/
using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    /// <summary>
    /// View model based on the Network model.
    /// </summary>
    public class NetworkViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Platform Type")]
        public PlatformType? PlatformType { get; set; }

        public decimal ContactsNumber { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
    }
}