/*
* Description: .
* Author: Zee
* Due date: 20/03/2018
*/
using HumanResourcesManagmentCapstone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesManagmentCapstone.ViewModel
{
    public class NetworkViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Platform Type")]
        public Platform? PlatformType { get; set; }

        [Display(Name = "Contacts Number")]
        public decimal ContactsNumber { get; set; }

        public int EmployeeId { get; set; }

        public string Employee { get; set; }
    }
}