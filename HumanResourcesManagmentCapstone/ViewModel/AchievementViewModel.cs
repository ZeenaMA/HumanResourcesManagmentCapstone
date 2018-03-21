/*
* Description: View model for Achievement passes information between Achievement views and its controller.
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
    /// <summary>
    /// View model based on the Achievement model.
    /// </summary>
    public class AchievementViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Achievement Type")]
        public AchievementType? AchievementType { get; set; }

        public string Discription { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }
    }
}